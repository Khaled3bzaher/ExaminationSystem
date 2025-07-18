﻿using Shared.Authentication;
using Shared.DTOs;

namespace ServicesAbstractions.Interfaces
{
    public interface IAuthenticationService
    {
        Task<APIResponse<UserResponse>> LoginAsync(LoginRequest loginRequest);
        Task<APIResponse<UserResponse>> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> CheckEmailAsync(string email);

    }
}
