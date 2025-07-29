using ExaminationSystem.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ServicesAbstractions.Interfaces;
using Shared.Authentication;
using Shared.DTOs.Exams;
using System.Security.Claims;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamsController(IServiceManager serviceManager, IHubContext<ExamHub> hubContext) : ControllerBase
    {
        [HttpGet("RequestExam")]
        public async Task<IActionResult> RequestExam(string studentId, Guid subjectId)
        {
            var response = await serviceManager.ExamService.RequestExam(studentId, subjectId);
            if (response.Success)
            {
                await hubContext.Clients.Group("Admins").SendAsync("StudentStartedExam", new StartExamNotificationDTO
                {
                    StudentName=response.Data!.StudentName,
                    SubjectName=response.Data!.SubjectName
                });
            }
            return response.ToActionResult();
        }
        [HttpGet("ExamsHistory")]
        public async Task<IActionResult> ExamsHistory([FromQuery]ExamHistoryParameters parameters)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (role == AppRoles.STUDENT)
            {
                parameters.studentId = userId;
            }
            var response = await serviceManager.ExamService.GetAllExamsHistory(parameters);
            return response.ToActionResult();
        }

        [HttpPost("SubmitExam")]
        public async Task<IActionResult> SubmitExam(StudentExamDTO examDTO)
        {
            var response = await serviceManager.ExamService.SubmitExam(examDTO);
            return response.ToActionResult();
        }
    }
}
