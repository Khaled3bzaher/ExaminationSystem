using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DTOs;
using Shared.DTOs.Subjects;
using Shared.QueryParameters;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<SubjectResponse>>> GetAllSubjects([FromQuery]SubjectQueryParameters parameters)
        {
            var subjects = await serviceManager.SubjectService.GetAllSubjectsAsync(parameters);
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
