using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
            => StatusCode(204,await serviceManager.AuthenticationService.LoginAsync(loginRequest));


        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerRequest));

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
            => Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));
    }
}
