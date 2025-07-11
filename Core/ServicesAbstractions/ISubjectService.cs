using Shared.DTOs.Subjects;

namespace ServicesAbstractions
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponse>> GetAllSubjectsAsync();
        Task<SubjectResponse> GetSubjectAsync(Guid id);
    }
}
