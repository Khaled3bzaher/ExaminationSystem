using Domain.Enums;
using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class StudentExam : BaseEntity<Guid>
    {
        public Subject Subject { get; set; }
        public Guid SubjectId { get; set; }
        public ApplicationUser Student { get; set; }
        public string StudentId { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public ExamStatus ExamStatus { get; set; } = ExamStatus.NotCompleted;
        public ICollection<ExamQuestion> ExamQuestions { get; set; } = [];
        public ExamResult? ExamResult { get; set; }
    }
}
