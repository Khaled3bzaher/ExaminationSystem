using EvaluationService.Domain.Models.BaseEntities;

namespace EvaluationService.Domain.Models
{
    public class Subject : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
