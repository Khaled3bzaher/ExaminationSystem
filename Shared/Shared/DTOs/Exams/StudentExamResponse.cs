namespace Shared.DTOs.Exams
{
    public class StudentExamResponse
    {
        public int DurationInMinutes { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        public ICollection<ExamQuestionResponse> Questions { get; set; }
    }
}
