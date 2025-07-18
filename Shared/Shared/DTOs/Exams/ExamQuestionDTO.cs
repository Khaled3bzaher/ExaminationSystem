using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Exams
{
    public class ExamQuestionDTO
    {
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public int SelectedChoiceId { get; set; }
    }
}
