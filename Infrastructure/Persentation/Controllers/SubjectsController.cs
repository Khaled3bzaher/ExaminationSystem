using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DTOs.Subjects;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetAllSubjects()
        {
            var subjects = await serviceManager.SubjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectResponse>> GetSubject(Guid id)
        {
            var subject = await serviceManager.SubjectService.GetSubjectAsync(id);
            return Ok(subject);
        }
    }
}
