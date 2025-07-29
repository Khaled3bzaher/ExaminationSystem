using EvaluationService.Domain.Models.BaseEntities;

namespace EvaluationService.Domain.Models
{
    public class ExamResult : BaseEntityPrimaryKey<Guid>
    {
        public Guid ExamId { get; set; }
        public double Result { get; set; } = 0;
        public DateTime EvaluatedAt { get; set; }
    }
}
