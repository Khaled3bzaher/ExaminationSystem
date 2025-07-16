using Shared.DTOs;
using Shared.DTOs.Students;
using Shared.QueryParameters;

namespace ServicesAbstractions.Interfaces
{
    public interface IStudentService
    {
        public Task<PaginatedResponse<StudentResponse>> GetAllStudentsAsync(StudentQueryParamters parameters);
        public Task<APIResponse<string>> ChangeStudentStatus(string studentId);
    }
}
