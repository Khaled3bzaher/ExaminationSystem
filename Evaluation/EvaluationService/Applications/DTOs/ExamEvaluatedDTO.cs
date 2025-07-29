using EvaluationService.Domain.Enums;

namespace EvaluationService.Applications.DTOs
{
    public class ExamEvaluatedDTO
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public ExamStatus ExamStatus { get; set; }
    }
}
