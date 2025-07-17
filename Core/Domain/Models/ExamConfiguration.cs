using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class ExamConfiguration : BaseEntity<int>
    {
        public Subject Subject { get; set; }
        public Guid SubjectId { get; set; }
        public int DurationInMinutes { get; set; } = 60;
        public int QuestionNumbers { get; set; } = 10;
        public int HardPercentage { get; set; } = 20;
        public int NormalPercentage { get; set; } = 30;
        public int LowPercentage { get; set; } = 50;
    }
}
