using Domain.Enums;
using Services.Specifications.Exams;
using Shared.DTOs.Admin;

namespace Services.Repositories
{
    public class AdminService(IUnitOfWork unitOfWork) : IAdminService
    {
        public async Task<APIResponse<StatsResponse>> GetStatsAsync()
        {
            var studentsCount = await unitOfWork.StudentRepository.CountAsync();
            var specifications = new StudentsExamsCountSpecifications(ExamStatus.Success);
            var examPassedCount = await unitOfWork.GetRepository<StudentExam, Guid>().CountAsync(specifications);
            specifications = new StudentsExamsCountSpecifications(ExamStatus.Failed);
            var examFailedCount = await unitOfWork.GetRepository<StudentExam, Guid>().CountAsync(specifications);
            var examCompletedCount = examPassedCount + examFailedCount;
            var statsResponse = new StatsResponse { 
                StudentsNumber= studentsCount,
                ExamCompleted = examCompletedCount,
                ExamFailed = examFailedCount,
                ExamPassed = examPassedCount,
            }; 
            return APIResponse<StatsResponse>.SuccessResponse(statsResponse);
        }
    }
}
