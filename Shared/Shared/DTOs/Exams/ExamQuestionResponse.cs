namespace Shared.DTOs.Exams
{
    public class ExamQuestionResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public ICollection<ExamQuestionChoiceResponse> Choices { get; set; }

    }
}
