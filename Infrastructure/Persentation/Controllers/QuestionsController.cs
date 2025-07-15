
using ServicesAbstractions.Interfaces;
using Shared.DTOs.Questions;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<QuestionResponse>>> GetAllQuestions([FromQuery] QuestionQueryParameters parameters)
        {
            var questions = await serviceManager.QuestionsService.GetAllQuestionsAsync(parameters);
            return Ok(questions);
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteQuestion(Guid Id)
        {
            var response = await serviceManager.QuestionsService.DeleteQuestionAsync(Id);
            return response.ToActionResult();
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDTO questionDTO)
        {
            var response = await serviceManager.QuestionsService.CreateQuestionAsync(questionDTO);
            return response.ToActionResult();
        }
    }
}
