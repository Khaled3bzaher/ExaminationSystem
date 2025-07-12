using Shared.DTOs.Subjects;
using Shared.QueryParameters;

namespace ServicesAbstractions
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponse>> GetAllSubjectsAsync(SubjectQueryParameters parameters);
        Task<SubjectResponse> GetSubjectAsync(Guid id);
    }
}
