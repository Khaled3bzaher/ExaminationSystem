using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Exams
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
