namespace Shared.DTOs.Exams
{
    public class StudentExamResponse
    {
        public Guid ExamId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;

        public ICollection<ExamQuestionResponse> Questions { get; set; }
    }
}
