using Shared.QueryParameters;
using System.Linq.Expressions;

namespace Services.Specifications.Subjects
{
    internal class SubjectSpecificationsCount(SubjectQueryParameters parameters) : BaseSpecifications<Subject>(CreateCriteria(parameters))
    {
        private static Expression<Func<Subject, bool>> CreateCriteria(SubjectQueryParameters parameters)
        {
            return p => string.IsNullOrWhiteSpace(parameters.search) || p.Name.ToLower().Contains(parameters.search.Trim().ToLower());
        }
    }
}
