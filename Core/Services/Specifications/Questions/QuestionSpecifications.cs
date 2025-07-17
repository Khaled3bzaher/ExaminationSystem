using Domain.Enums;
using Shared.Options.SortingOptions;
using System.Linq.Expressions;

namespace Services.Specifications.Questions
{
    internal class QuestionSpecifications : BaseSpecifications<Question>
    {
        public QuestionSpecifications(QuestionQueryParameters parameters) : base(CreateCriteria(parameters))
        {
            ApplySorting(parameters.sorting);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }
        public QuestionSpecifications(Guid subjectId,DifficultyLevel level,int questionCount) : base(s=>s.SubjectId==subjectId && s.QuestionLevel==level)
        {
            AddOrderBy(q => Guid.NewGuid());
            AddInclude(q=>q.Choices);
            ApplyTaking(questionCount);
        }
        public QuestionSpecifications(Guid subjectId, int questionCount, List<Guid> currentQuestionsIds) : base(s => s.SubjectId == subjectId && !currentQuestionsIds.Contains(s.Id))
        {
            AddOrderBy(q => Guid.NewGuid());
            AddInclude(q => q.Choices);
            ApplyTaking(questionCount);
        }

        private static Expression<Func<Question, bool>> CreateCriteria(QuestionQueryParameters parameters)
        {
            return p =>
            (string.IsNullOrWhiteSpace(parameters.search) || p.Text.ToLower().Contains(parameters.search.Trim().ToLower())) &&
            (!parameters.SubjectId.HasValue || p.SubjectId == parameters.SubjectId.Value);
        }

        private void ApplySorting(QuestionsSortOptions sorting)
        {
            switch (sorting)
            {
                case QuestionsSortOptions.NameAsc:
                    AddOrderBy(p => p.Text);
                    break;
                case QuestionsSortOptions.NameDesc:
                    AddOrderByDescending(p => p.Text);
                    break;
                case QuestionsSortOptions.CreatedAtAsc:
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case QuestionsSortOptions.CreatedAtDesc:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                case QuestionsSortOptions.LevelAsc:
                    AddOrderBy(p => p.QuestionLevel);
                    break;
                case QuestionsSortOptions.LevelDesc:
                    AddOrderByDescending(p => p.QuestionLevel);
                    break;
                default:
                    AddOrderBy(p => p.CreatedAt);
                    break;
            }
        }
    }
}
