namespace Shared.DTOs.Exams
{
    public class ExamEvaluatedResponse
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public string ExamStatus { get; set; }
    }
}
