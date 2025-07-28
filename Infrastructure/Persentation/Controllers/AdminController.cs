using Microsoft.AspNetCore.Authorization;
using ServicesAbstractions.Interfaces;
using Shared.Authentication;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AppPolicy.ADMIN_POLICY)]

    public class AdminController(IServiceManager serviceManager) : ControllerBase 
    {
        [HttpGet("Stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var response = await serviceManager.AdminService.GetStatsAsync();
            return response.ToActionResult();
        }
    }
}
