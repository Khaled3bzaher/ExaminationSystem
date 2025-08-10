
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specifications.Questions;
using Shared.DTOs.Questions;

namespace Services.Repositories
{
    internal class QuestionsService(IUnitOfWork unitOfWork,IMapper mapper) : IQuestionsService
    {
        public async Task<APIResponse<string>> CreateQuestionAsync(CreateQuestionDTO questionDTO)
        {
            try
            {
                var subject = await unitOfWork.GetRepository<Subject, Guid>().isExists(questionDTO.SubjectId);
                if (!subject)
                    return APIResponse<string>.FailureResponse($"Subject with Id: {questionDTO.SubjectId} Not Found..!", (int)HttpStatusCode.NotFound);

                var QuestionMap = mapper.Map<Question>(questionDTO);
                await unitOfWork.GetRepository<Question, Guid>().AddAsync(QuestionMap);
                await unitOfWork.SaveChangesAsync();
                return APIResponse<string>.SuccessResponse(null, message: $"Question Successfully Created");
            }
            catch (Exception ex)
            {
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
            }
                
        }

        public async Task<APIResponse<string>> DeleteQuestionAsync(Guid questionId)
        {
            try
            {
                var question = await unitOfWork.GetRepository<Question, Guid>().GetAsync(questionId);
                if (question is null)
                    return APIResponse<string>.FailureResponse($"Question with Id: {questionId} Not Found..!", (int)HttpStatusCode.NotFound);

                unitOfWork.GetRepository<Question, Guid>().Delete(question);
                await unitOfWork.SaveChangesAsync();
                return APIResponse<string>.SuccessResponse(null, message: $"Question {question.Text} Successfully Deleted");
            }catch(Exception ex)
            {
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
            }
               
        }

        public async Task<PaginatedResponse<QuestionResponse>> GetAllQuestionsAsync(QuestionQueryParameters parameters)
        {
            var specifications = new QuestionSpecifications(parameters);
            var questions = await unitOfWork.GetRepository<Question, Guid>().GetAllProjectedAsync<QuestionResponse>(specifications);
            var questionsCount = questions.Count();
            var totalCount = await unitOfWork.GetRepository<Question, Guid>().CountAsync(new QuestionSpecificationsCount(parameters));
            return new(parameters.PageIndex, questionsCount, totalCount, questions);
        }
    }
}
