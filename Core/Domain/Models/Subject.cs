using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class Subject : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ExamConfiguration ExamConfiguration { get; set; }
    }
}
