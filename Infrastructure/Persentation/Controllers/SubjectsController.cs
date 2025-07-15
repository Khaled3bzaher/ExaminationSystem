

using ServicesAbstractions.Interfaces;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<SubjectResponse>>> GetAllSubjects([FromQuery] SubjectQueryParameters parameters)
        {
            var subjects = await serviceManager.SubjectService.GetAllSubjectsAsync(parameters);
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(Guid id)
        {
            var response = await serviceManager.SubjectService.GetSubjectAsync(id);
            return response.ToActionResult();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.CreateSubjectAsync(subject);
            return response.ToActionResult();
        }
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateSubject(Guid Id, [FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.UpdateSubjectAsync(Id, subject);
            return response.ToActionResult();
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteSubject(Guid Id)
        {
            var response = await serviceManager.SubjectService.DeleteSubjectAsync(Id);
            return response.ToActionResult();
        }
    }
}
