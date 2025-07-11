using Domain.Models.BaseEntities;

namespace Domain.Models
{
    public class ExamQuestion : BaseEntityPrimaryKey<Guid>
    {
        public StudentExam Exam { get; set; }
        public Guid ExamId{ get; set; }
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionChoice? SelectedChoice { get; set; }
        public int? SelectedChoiceId { get; set; }


    }
}
