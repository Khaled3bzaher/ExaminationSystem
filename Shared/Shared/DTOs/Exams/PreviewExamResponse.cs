namespace Shared.DTOs.Exams
{
    public class PreviewExamResponse
    {
        public string SubjectName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string ExamStatus { get; set; }
        public double Result { get; set; }
        public List<PreviewExamQuestionResponse> Questions { get; set; }
    }
}
