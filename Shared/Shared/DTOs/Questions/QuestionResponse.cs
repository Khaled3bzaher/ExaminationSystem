using Shared.DTOs.QuestionChoices;

namespace Shared.DTOs.Questions
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string QuestionLevel { get; set; }
        public IEnumerable<QuestionChoiceResponse> Choices { get; set; }
    }
}
