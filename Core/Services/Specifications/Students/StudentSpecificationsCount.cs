using System.Linq.Expressions;

namespace Services.Specifications.Students
{
    internal class StudentSpecificationsCount(StudentQueryParamters parameters) : BaseSpecifications<ApplicationUser>(CreateCriteria(parameters))
    {
        private static Expression<Func<ApplicationUser, bool>> CreateCriteria(StudentQueryParamters parameters)
        {
            return p => (string.IsNullOrWhiteSpace(parameters.search) || p.FullName.ToLower().Contains(parameters.search.Trim().ToLower()));
        }
    }
}
