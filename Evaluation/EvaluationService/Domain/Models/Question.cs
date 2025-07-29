using EvaluationService.Domain.Enums;
using EvaluationService.Domain.Models.BaseEntities;

namespace EvaluationService.Domain.Models
{
    public class Question : BaseEntity<Guid>
    {
        public string Text { get; set; }
        public DifficultyLevel QuestionLevel { get; set; }
        public Subject Subject { get; set; }

        public Guid SubjectId { get; set; }

        public ICollection<QuestionChoice> Choices { get; set; }
    }
}
