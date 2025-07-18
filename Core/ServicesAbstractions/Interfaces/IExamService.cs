using Shared.DTOs;
using Shared.DTOs.Exams;
using Shared.QueryParameters;

namespace ServicesAbstractions.Interfaces
{
    public interface IExamService
    {
        Task<APIResponse<StudentExamResponse>> RequestExam(string studentId, Guid subjectId);
        Task<APIResponse<PaginatedResponse<ExamHistoryResponse>>> GetAllExamsHistory(ExamHistoryParameters parameters);
        Task<APIResponse<string>> SubmitExam(StudentExamDTO examDTO);
    }
}
