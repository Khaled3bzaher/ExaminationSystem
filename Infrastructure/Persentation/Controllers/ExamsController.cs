using ServicesAbstractions.Interfaces;
using Shared.DTOs.Exams;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("RequestExam")]
        public async Task<IActionResult> RequestExam(string studentId, Guid subjectId)
        {
            var response = await serviceManager.ExamService.RequestExam(studentId, subjectId);
            return response.ToActionResult();
        }
        [HttpGet("ExamsHistory")]
        public async Task<IActionResult> ExamsHistory([FromQuery]ExamHistoryParameters parameters)
        {
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
