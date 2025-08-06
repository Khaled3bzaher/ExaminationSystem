using Shared.DTOs;
using Shared.DTOs.Subjects;
using Shared.QueryParameters;

namespace ServicesAbstractions.Interfaces
{
    public interface ISubjectService
    {
        Task<PaginatedResponse<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters);
        Task<APIResponse<SubjectResponse>> GetSubjectAsync(Guid id);
        Task<APIResponse<string>> CreateSubjectAsync(SubjectDTO subject);
        Task<APIResponse<string>> UpdateSubjectAsync(Guid subjectId,SubjectDTO subject);
        Task<APIResponse<string>> DeleteSubjectAsync(Guid subjectId);
        Task<APIResponse<string>> UpdateSubjectConfigurationAsync(Guid subjectId, SubjectConfigurationDTO configurationDTO);
        Task<APIResponse<PaginatedResponse<SubjectConfigurationResponse>>> GetSubjectsConfigurationAsync(SubjectConfigurationQueryParameters parameters);
    }
}
