using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Exams
{
    public class StudentExamDTO
    {
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public ICollection<ExamQuestionDTO> Questions { get; set; }
    }
}
