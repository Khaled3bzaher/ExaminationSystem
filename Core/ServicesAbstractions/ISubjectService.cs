using Shared.DTOs;
using Shared.DTOs.Subjects;
using Shared.QueryParameters;

namespace ServicesAbstractions
{
    public interface ISubjectService
    {
        Task<PaginatedResponse<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters);
        Task<APIResponse<SubjectResponse>> GetSubjectAsync(Guid id);
        Task<APIResponse<string>> CreateSubjectAsync(SubjectDTO subject);
        Task<APIResponse<string>> UpdateSubjectAsync(Guid subjectId,SubjectDTO subject);
        Task<APIResponse<string>> DeleteSubjectAsync(Guid subjectId);
    }
}
