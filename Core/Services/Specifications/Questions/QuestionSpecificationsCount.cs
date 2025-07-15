using System.Linq.Expressions;

namespace Services.Specifications.Questions
{
    internal class QuestionSpecificationsCount(QuestionQueryParameters parameters) : BaseSpecifications<Question>(CreateCriteria(parameters))
    {
        private static Expression<Func<Question, bool>> CreateCriteria(QuestionQueryParameters parameters)
        {
            return p =>
            (string.IsNullOrWhiteSpace(parameters.search) || p.Text.ToLower().Contains(parameters.search.Trim().ToLower())) &&
            (!parameters.SubjectId.HasValue || p.SubjectId == parameters.SubjectId.Value);
        }
    }
}
