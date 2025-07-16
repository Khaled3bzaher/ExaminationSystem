using ServicesAbstractions.Interfaces;
using Shared.DTOs.Students;

namespace Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<StudentResponse>>> GetAllStudents([FromQuery] StudentQueryParamters parameters)
        {
            var students = await serviceManager.StudentService.GetAllStudentsAsync(parameters);
            return Ok(students);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> ChangeStudentStatus(string Id)
        {
            var response = await serviceManager.StudentService.ChangeStudentStatus(Id);
            return response.ToActionResult();
        }
    }
}
