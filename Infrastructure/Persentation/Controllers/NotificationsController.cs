using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Services.Repositories;
using Shared.Authentication;
using System.Security.Claims;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController(INotificationService notificationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications([FromQuery]NotificationParameters notificationParameters)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (role == AppRoles.STUDENT)
            {
                notificationParameters.studentId = userId;
            }

            var response = await notificationService.GetAllNotifications(notificationParameters);
            return response.ToActionResult();
        }
        [HttpPut("{Id}")]
        [Authorize(Policy = AppPolicy.STUDENT_POLICY)]
        public async Task<IActionResult> MarkNotificationAsRead(string Id)
        {
            var respone = await notificationService.MarkNotificationAsRead(Id);
            return respone.ToActionResult();
        }
    }
}
