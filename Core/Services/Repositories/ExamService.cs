using Domain.Enums;
using Domain.Models;
using Services.Specifications.Exams;
using Services.Specifications.Questions;
using Shared.DTOs;
using Shared.DTOs.Exams;

namespace Persistence.Repositories
{
    public class ExamService(IUnitOfWork unitOfWork) : IExamService
    {
        public async Task<APIResponse<StudentExamResponse>> RequestExam(string studentId, Guid subjectId)
        {
            var studentExists = await unitOfWork.StudentRepository.isStudentExistsAsync(studentId);
            if(!studentExists)
                return APIResponse<StudentExamResponse>.FailureResponse($"Student with Id: {studentId} Not Found..!", (int)HttpStatusCode.NotFound);

            var configurations = await GetSubjectConfiguration(subjectId);
            if(configurations == null)
                return APIResponse<StudentExamResponse>.FailureResponse($"Subject with Id: {subjectId} Not Found..!", (int)HttpStatusCode.NotFound);

            var enteredBefore = await GetStudentSubjectExamRecord(studentId, subjectId);
            if (enteredBefore is not null && enteredBefore.ExamStatus == ExamStatus.Success)
                return APIResponse<StudentExamResponse>.FailureResponse($"You Already Had this Exam..!", (int)HttpStatusCode.Conflict);
            else if(enteredBefore is not null && enteredBefore.ExamStatus == ExamStatus.NotCompleted)
                unitOfWork.GetRepository<StudentExam, Guid>().Delete(enteredBefore);

            if (!await QuestionNumbersIsValid(subjectId, configurations.QuestionNumbers))
                return APIResponse<StudentExamResponse>.FailureResponse($"Cannot Request Exam Now, Contact the Administrator..!", (int)HttpStatusCode.InternalServerError);
            else
            {
                var allQuestions = await GenerateAllExamQuestions(configurations, subjectId);
                if (allQuestions.Count() != configurations.QuestionNumbers)
                {
                    var questionsIds = allQuestions.Select(x => x.Id).ToList();
                    var remainQuestionsCount = configurations.QuestionNumbers - questionsIds.Count();
                    var reminingQuestions = await GetRemainQuestions(remainQuestionsCount, subjectId, questionsIds);
                    if (allQuestions.Count() + reminingQuestions.Count() != configurations.QuestionNumbers)
                        return APIResponse<StudentExamResponse>.FailureResponse($"Cannot Request Exam Now, Contact the Administrator..!", (int)HttpStatusCode.InternalServerError);
                    else
                        allQuestions = allQuestions.Concat(reminingQuestions).OrderBy(x => Guid.NewGuid()).ToList();
                }

                var generatedExam = new StudentExamResponse()
                {
                    DurationInMinutes = configurations.DurationInMinutes,
                    Questions = allQuestions,
                };
                var studentExam = new StudentExam()
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                };
                await unitOfWork.GetRepository<StudentExam, Guid>().AddAsync(studentExam);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return APIResponse<StudentExamResponse>.SuccessResponse(generatedExam);
                else
                    return APIResponse<StudentExamResponse>.FailureResponse("Something went wrong While Request Exam..!");

            }
        }
        public async Task<APIResponse<PaginatedResponse<ExamHistoryResponse>>> GetAllExamsHistory(ExamHistoryParameters parameters)
        {
            var specificationsCount = new StudentsExamsCountSpecifications(parameters);
            var examHistoryCount = await unitOfWork.GetRepository<StudentExam, Guid>().CountAsync(specificationsCount);
            if (examHistoryCount == 0)
                return APIResponse<PaginatedResponse<ExamHistoryResponse>>.FailureResponse($"No Exams Entered..!", (int)HttpStatusCode.NotFound);

                var specifications = new StudentsExamsSpecifications(parameters);
                var studentsExamHistory = await unitOfWork.GetRepository<StudentExam,Guid>().GetAllProjectedAsync<ExamHistoryResponse>(specifications);
                var pageCount = studentsExamHistory.Count();
            var paginatedData = new PaginatedResponse<ExamHistoryResponse>(parameters.PageIndex, pageCount, examHistoryCount, studentsExamHistory);
            return APIResponse<PaginatedResponse<ExamHistoryResponse>>.SuccessResponse(paginatedData);
        }
        private async Task<SubjectConfigurationResponse?> GetSubjectConfiguration(Guid subjectId)
        {
            var specifications = new ExamConfigurationSpecifications(subjectId);
            return await unitOfWork.GetRepository<ExamConfiguration,int>().GetProjectedAsync<SubjectConfigurationResponse>(specifications);
        }
        private async Task<bool> QuestionNumbersIsValid(Guid subjectId,int questionNumbers)
        {
            var specificationsCount = new QuestionSpecificationsCount(subjectId);
            var subjectQuestionsCount = await unitOfWork.GetRepository<Question, Guid>().CountAsync(specificationsCount);
            if (subjectQuestionsCount >= questionNumbers)
                return true;
            else
                return false;
        }
        private async Task<ICollection<ExamQuestionResponse>> GenerateAllExamQuestions(SubjectConfigurationResponse configurations,Guid subjectId)
        {
            var allQuestions = new List<ExamQuestionResponse>();
            var hardQuestionsCount = CalculateCountFromPercentageAndTotalQuestionsNumber(configurations.QuestionNumbers, configurations.HardPercentage);
            var normalQuestionsCount = CalculateCountFromPercentageAndTotalQuestionsNumber(configurations.QuestionNumbers, configurations.NormalPercentage);
            var lowQuestionsCount = CalculateCountFromPercentageAndTotalQuestionsNumber(configurations.QuestionNumbers, configurations.LowPercentage);

            var hardQuestions = await GetQuestionsByDiffculty(DifficultyLevel.High, hardQuestionsCount, subjectId);
            var normalQuestions = await GetQuestionsByDiffculty(DifficultyLevel.Normal, normalQuestionsCount, subjectId);
            var lowQuestions = await GetQuestionsByDiffculty(DifficultyLevel.Low, lowQuestionsCount, subjectId);

            if (hardQuestions.Count() + normalQuestions.Count() + lowQuestions.Count() != configurations.QuestionNumbers)
                return allQuestions;

            allQuestions.AddRange(hardQuestions);
            allQuestions.AddRange(normalQuestions);
            allQuestions.AddRange(lowQuestions);
            return allQuestions.OrderBy(x => Guid.NewGuid()).ToList();
        }
        private async Task<IEnumerable<ExamQuestionResponse>> GetQuestionsByDiffculty(DifficultyLevel level,int questionsCount,Guid subjectId)
        {
            var specifications = new QuestionSpecifications(subjectId,level,questionsCount);
            return await unitOfWork.GetRepository<Question, Guid>().GetAllProjectedAsync<ExamQuestionResponse>(specifications);
        }
        private async Task<IEnumerable<ExamQuestionResponse>> GetRemainQuestions(int questionsCount, Guid subjectId,List<Guid> currentQuestionsIds)
        {
            var specifications = new QuestionSpecifications(subjectId, questionsCount, currentQuestionsIds);
            return await unitOfWork.GetRepository<Question, Guid>().GetAllProjectedAsync<ExamQuestionResponse>(specifications);
        }
        private int CalculateCountFromPercentageAndTotalQuestionsNumber(int totalQuestionsNumber, int levelPercentage)
            => (int)Math.Round((levelPercentage / 100.0) * totalQuestionsNumber);
        private async Task<StudentExam?> GetStudentSubjectExamRecord(string studentId, Guid subjectId)
        {
            var studentExamSpecifications = new StudentsExamsSpecifications(studentId, subjectId);
            return await unitOfWork.GetRepository<StudentExam, Guid>().GetAsync(studentExamSpecifications);
        }
    }
}
