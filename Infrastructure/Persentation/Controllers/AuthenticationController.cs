using Microsoft.AspNetCore.Mvc;
using Persentation.Extensions;
using ServicesAbstractions;
using Shared.Authentication;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await serviceManager.AuthenticationService.LoginAsync(loginRequest);
            return response.ToActionResult();
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var response = await serviceManager.AuthenticationService.RegisterAsync(registerRequest);
            return response.ToActionResult();
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
            => Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));
    }
}
