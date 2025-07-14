using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Authentication;
using Shared.DTOs;
using Shared.DTOs.Subjects;
using Shared.QueryParameters;
using System.Security.Claims;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = AppRoles.STUDENT)]
        public async Task<ActionResult<PaginatedResponse<SubjectResponse>>> GetAllSubjects([FromQuery] SubjectQueryParameters parameters)
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
        [HttpPost]
        public async Task<ActionResult<APIResponse<string>>> CreateSubject([FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.CreateSubjectAsync(subject);
            return Ok(response);
        }
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<APIResponse<string>>> UpdateSubject(Guid Id,[FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.UpdateSubjectAsync(Id,subject);
            return Ok(response);
        }
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<APIResponse<string>>> DeleteSubject(Guid Id)
        {
            var response = await serviceManager.SubjectService.DeleteSubjectAsync(Id);
            return Ok(response);
        }
    }
}
