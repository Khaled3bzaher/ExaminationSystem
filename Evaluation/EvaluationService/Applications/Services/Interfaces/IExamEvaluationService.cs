using EvaluationService.Applications.DTOs;

namespace EvaluationService.Applications.Services.Interfaces
{
    public interface IExamEvaluationService
    {
        /// <summary>
        /// Evaluates the exam for a student.
        /// </summary>
        /// <param name="studentExam">The student exam details.</param>
        /// <returns>The score of the evaluated exam.</returns>
        Task<ExamEvaluatedDTO> EvaluateExamAsync(StudentExamDTO studentExam);
    }
}
