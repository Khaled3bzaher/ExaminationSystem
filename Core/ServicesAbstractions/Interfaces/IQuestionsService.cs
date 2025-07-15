using Shared.DTOs;
using Shared.DTOs.Questions;
using Shared.DTOs.Subjects;
using Shared.QueryParameters;

namespace ServicesAbstractions.Interfaces
{
    public interface IQuestionsService
    {
        public Task<PaginatedResponse<QuestionResponse>> GetAllQuestionsAsync(QuestionQueryParameters parameters);
        Task<APIResponse<string>> DeleteQuestionAsync(Guid questionId);
        Task<APIResponse<string>> CreateQuestionAsync(CreateQuestionDTO questionDTO);


    }
}
