namespace Shared.DTOs.Exams
{
    public class PreviewExamQuestionResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<ExamQuestionChoiceResponse> Choices { get; set; }
        public int CorrectAnswerId { get; set; }
        public int SelectedChoiceId { get; set; }
    }
}
