using Domain.Models;
using Services.Specifications.Questions;
using Services.Specifications.Students;
using Shared.DTOs.Students;

namespace Services.Repositories
{
    internal class StudentService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IStudentService
    {
        public async Task<APIResponse<string>> ChangeStudentStatus(string studentId)
        {
            var student = await userManager.FindByIdAsync(studentId);
            if (student == null)
                return APIResponse<string>.FailureResponse($"Student with Id: {studentId} Not Found..!", (int)HttpStatusCode.NotFound);
            student.IsActive = !student.IsActive;
            var result = await userManager.UpdateAsync(student);
            if (result.Succeeded)
                return APIResponse<string>.SuccessResponse(null, $"{student.FullName} IsActive {student.IsActive} Now..!");
            else
                return APIResponse<string>.FailureResponse("Something went wrong While Saving..!");
        }

        public async Task<PaginatedResponse<StudentResponse>> GetAllStudentsAsync(StudentQueryParamters parameters)
        {
            var specifications = new StudentSpecifications(parameters);
            var totalCount = await unitOfWork.StudentRepository.CountAsync(new StudentSpecificationsCount(parameters));
            if(totalCount==0)
                return new(parameters.PageIndex, 0, 0, null);

            var students = await unitOfWork.StudentRepository.GetAllProjectedAsync<StudentResponse>(specifications);
            var studentsCount = students.Count();
            return new(parameters.PageIndex, studentsCount, totalCount, students);
        }
    }
}
