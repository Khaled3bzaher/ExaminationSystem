using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Authentication;
using System.Security.Claims;

namespace ExaminationSystem.Web.Hubs
{
    public class ExamHub(ILogger<ExamHub> logger) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var userRole = Context.User?.Claims.Where(c => c.Type == ClaimTypes.Role && c.Value == AppRoles.ADMIN).Select(c => c.Value).FirstOrDefault();
            
            if (userRole != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
                logger.LogInformation($"Admin {userId} added to Admins group");
            }
            await base.OnConnectedAsync();
        }
    }
}
