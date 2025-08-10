using Microsoft.EntityFrameworkCore;

namespace Services.Repositories
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,IOptions<JWTOptions> jwtOptions) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        => await userManager.FindByEmailAsync(email) != null;
        
        public async Task<APIResponse<UserResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var userFound = await userManager.FindByEmailAsync(loginRequest.Email);
            if (userFound is null)
                return APIResponse<UserResponse>.FailureResponse($"User with Email: {loginRequest.Email} Not Found..!", (int)HttpStatusCode.NotFound);

            var loginValid = await userManager.CheckPasswordAsync(userFound, loginRequest.Password);
            if (loginValid)
                if(!userFound.IsActive) // For User Set Active
                    return APIResponse<UserResponse>.FailureResponse($"User {userFound.FullName} is Disabled", (int)HttpStatusCode.Unauthorized);
                else
                    return APIResponse<UserResponse>.SuccessResponse(new UserResponse(loginRequest.Email, userFound.FullName, await CreateTokenAsync(userFound)));

            return APIResponse<UserResponse>.FailureResponse("Invalid Email Or Password", (int)HttpStatusCode.Unauthorized);
        }

        public async Task<APIResponse<UserResponse>> RegisterAsync(RegisterRequest registerRequest)
        {
            using var transaction = await unitOfWork.BeginTransactionAsync();
            try
            {
                if (await userManager.Users.AnyAsync(u => u.PhoneNumber == registerRequest.PhoneNumber))
                {
                    return APIResponse<UserResponse>.FailureResponse("Phone Number already exists", (int)HttpStatusCode.Conflict);
                }
                var user = new ApplicationUser
                {
                    Email = registerRequest.Email,
                    FullName = registerRequest.Name,
                    PhoneNumber = registerRequest.PhoneNumber,
                    UserName = registerRequest.Email
                };
                var result = await userManager.CreateAsync(user, registerRequest.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return APIResponse<UserResponse>.FailureResponse(errors.FirstOrDefault(), (int)HttpStatusCode.Conflict);
                }
                var roleResult = await userManager.AddToRoleAsync(user, AppRoles.STUDENT);
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return APIResponse<UserResponse>.FailureResponse("Internal Server Error");

                }

                await transaction.CommitAsync();
                return APIResponse<UserResponse>.SuccessResponse(new UserResponse(registerRequest.Email, registerRequest.Name, await CreateTokenAsync(user)));

            }catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return APIResponse<UserResponse>.FailureResponse("Internal Server Error");
            }

        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwt = jwtOptions.Value;
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,user.Id),
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.FullName!),
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles) claims.Add(new(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(jwt.DurationInHours),
                signingCredentials:creds
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
