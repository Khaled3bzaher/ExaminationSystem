using Domain.Enums;
using Domain.Models.BaseEntities;

namespace Domain.Models
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
