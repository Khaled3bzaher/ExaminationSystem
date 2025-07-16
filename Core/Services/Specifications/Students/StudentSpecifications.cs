using Shared.Options.SortingOptions;
using System.Linq.Expressions;

namespace Services.Specifications.Students
{
    internal class StudentSpecifications : BaseSpecifications<ApplicationUser>
    {
        public StudentSpecifications(StudentQueryParamters parameters) : base(CreateCriteria(parameters))
        {
            ApplySorting(parameters.sorting);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }
        private static Expression<Func<ApplicationUser, bool>> CreateCriteria(StudentQueryParamters parameters)
        {
            return p => (string.IsNullOrWhiteSpace(parameters.search) || p.FullName.ToLower().Contains(parameters.search.Trim().ToLower()));
        }
        private void ApplySorting(StudentSortOptions sorting)
        {
            switch (sorting)
            {
                case StudentSortOptions.NameAsc:
                    AddOrderBy(p => p.FullName);
                    break;
                case StudentSortOptions.NameDesc:
                    AddOrderByDescending(p => p.FullName);
                    break;
                case StudentSortOptions.ActiveAsc:
                    AddOrderBy(p => p.IsActive);
                    break;
                case StudentSortOptions.ActiveDesc:
                    AddOrderByDescending(p => p.IsActive);
                    break;
                default:
                    AddOrderBy(p => p.FullName);
                    break;
            }
        }
    }
}
