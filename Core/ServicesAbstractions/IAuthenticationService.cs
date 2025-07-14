using Shared.Authentication;

namespace ServicesAbstractions
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> CheckEmailAsync(string email);

    }
}
