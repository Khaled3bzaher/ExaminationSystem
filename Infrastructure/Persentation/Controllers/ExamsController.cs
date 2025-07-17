using ServicesAbstractions.Interfaces;

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

    }
}
