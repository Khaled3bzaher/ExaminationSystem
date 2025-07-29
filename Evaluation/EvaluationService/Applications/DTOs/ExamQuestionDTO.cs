using System.ComponentModel.DataAnnotations;

namespace EvaluationService.Applications.DTOs
{
    public class ExamQuestionDTO
    {
        [Required]
        public Guid QuestionId { get; set; }
        public int? SelectedChoiceId { get; set; }
    }
}
