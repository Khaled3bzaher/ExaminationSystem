

using Microsoft.AspNetCore.Authorization;
using ServicesAbstractions.Interfaces;
using Shared.Authentication;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<SubjectResponse>>> GetAllSubjects([FromQuery] SubjectQueryParameters parameters)
        {
            var subjects = await serviceManager.SubjectService.GetAllSubjectsAsync(parameters);
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        [Authorize(Policy =AppPolicy.ADMIN_POLICY)]
        public async Task<IActionResult> GetSubject(Guid id)
        {
            var response = await serviceManager.SubjectService.GetSubjectAsync(id);
            return response.ToActionResult();
        }
        [HttpPost]
        [Authorize(Policy = AppPolicy.ADMIN_POLICY)]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.CreateSubjectAsync(subject);
            return response.ToActionResult();
        }
        [HttpPut("{Id:guid}")]
        [Authorize(Policy = AppPolicy.ADMIN_POLICY)]

        public async Task<IActionResult> UpdateSubject(Guid Id, [FromBody] SubjectDTO subject)
        {
            var response = await serviceManager.SubjectService.UpdateSubjectAsync(Id, subject);
            return response.ToActionResult();
        }
        [HttpDelete("{Id:guid}")]
        [Authorize(Policy = AppPolicy.ADMIN_POLICY)]

        public async Task<IActionResult> DeleteSubject(Guid Id)
        {
            var response = await serviceManager.SubjectService.DeleteSubjectAsync(Id);
            return response.ToActionResult();
        }
        [HttpPut("Configurations")]
        [Authorize(Policy = AppPolicy.ADMIN_POLICY)]

        public async Task<IActionResult> UpdateSubjectConfigurations(Guid Id, [FromBody] SubjectConfigurationDTO configurationDTO)
        {
            var response = await serviceManager.SubjectService.UpdateSubjectConfigurationAsync(Id, configurationDTO);
            return response.ToActionResult();
        }
    }
}
