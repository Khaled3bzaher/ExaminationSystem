using Shared.DTOs.QuestionChoices;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Questions
{
    public class CreateQuestionDTO
    {
        [Required(ErrorMessage = "Subject Id is Required")]
        public Guid SubjectId { get; set; }
        [Required(ErrorMessage ="Question Text is Required")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Question Level is Required")]
        public int QuestionLevel { get; set; }
        public ICollection<QuestionChoiceDTO> Choices { get; set; } = [];
    }
}
