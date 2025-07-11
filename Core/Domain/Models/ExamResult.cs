using Domain.Enums;
using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class ExamResult : BaseEntityPrimaryKey<Guid>
    {
        public StudentExam Exam { get; set; }
        public Guid ExamId { get; set; }
        public double Result { get; set; } = 0;
        public ResultsStatus ResultsStatus { get; set; }
        public DateTime EvaluatedAt { get; set; }
    }
}
