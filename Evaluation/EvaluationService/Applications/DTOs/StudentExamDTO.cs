using System.ComponentModel.DataAnnotations;

namespace EvaluationService.Applications.DTOs
{
    public class StudentExamDTO
    {
        public DateTime SubmittedAt { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public ICollection<ExamQuestionDTO> Questions { get; set; }
    }
}
