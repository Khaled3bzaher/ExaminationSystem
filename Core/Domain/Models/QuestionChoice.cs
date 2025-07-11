using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class QuestionChoice : BaseEntity<int>
    {
        public string Text { get; set; }
        public bool isCorrect { get; set; }=false;

        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
