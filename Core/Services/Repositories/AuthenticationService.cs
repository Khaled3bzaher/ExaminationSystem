using ServicesAbstractions.Interfaces;
using Shared.DTOs;
using System.Net;

namespace Services.Repositories
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager, IOptions<JWTOptions> jwtOptions) : IAuthenticationService
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
            var user = new ApplicationUser
            {
                Email = registerRequest.Email,
                FullName = registerRequest.Name,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.Email
            };
            var result = await userManager.CreateAsync(user,registerRequest.Password);
            var roleResult = await userManager.AddToRoleAsync(user, AppRoles.STUDENT);
            if (result.Succeeded && roleResult.Succeeded)
                return APIResponse<UserResponse>.SuccessResponse(new UserResponse(registerRequest.Email, registerRequest.Name, await CreateTokenAsync(user)));
           
            var errors = result.Errors.Select(e => e.Description).ToList();

            return APIResponse<UserResponse>.FailureResponse(errors.FirstOrDefault(),(int)HttpStatusCode.Conflict);
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwt = jwtOptions.Value;
            var claims = new List<Claim>()
            {
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
                expires: DateTime.UtcNow.AddHours(jwt.DurationInHours),
                signingCredentials:creds
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
