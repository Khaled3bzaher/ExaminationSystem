using ServicesAbstractions.Interfaces;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
