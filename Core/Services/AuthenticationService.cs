
using System.Text;

namespace Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager, IOptions<JWTOptions> jwtOptions) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        => (await userManager.FindByEmailAsync(email)) != null;
        
        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var userFound = await userManager.FindByEmailAsync(loginRequest.Email)
                ?? throw new NotFoundException($"User with Email: {loginRequest.Email} Not Found..!");

            var loginValid = await userManager.CheckPasswordAsync(userFound, loginRequest.Password);
            if (loginValid && userFound.IsActive) // For User Set Active
                return new(loginRequest.Email, userFound.FullName, await CreateTokenAsync(userFound));
            throw new UnauthorizedException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
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
                return new(registerRequest.Email, registerRequest.Name, await CreateTokenAsync(user));
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors);
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
