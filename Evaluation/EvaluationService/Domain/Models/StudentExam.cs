using EvaluationService.Domain.Enums;
using EvaluationService.Domain.Models.BaseEntities;

namespace EvaluationService.Domain.Models
{
    public class StudentExam : BaseEntity<Guid>
    {
        public Subject Subject { get; set; }

        public Guid SubjectId { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public DateTime? SubmittedAt { get; set; }
        public ExamStatus ExamStatus { get; set; } = ExamStatus.NotCompleted;
    }
}
