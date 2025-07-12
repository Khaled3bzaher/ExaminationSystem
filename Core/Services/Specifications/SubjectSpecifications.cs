
using Shared.QueryParameters;
using System.Linq.Expressions;

namespace Services.Specifications
{
    internal class SubjectSpecifications : BaseSpecifications<Subject>
    {
        public SubjectSpecifications(Guid id) : base(subject =>subject.Id==id)
        {
        }
        public SubjectSpecifications(SubjectQueryParameters parameters) : base(CreateCriteria(parameters))
        {
            ApplySorting(parameters.sorting);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }

        private static Expression<Func<Subject,bool>> CreateCriteria(SubjectQueryParameters parameters)
        {
            return p => (string.IsNullOrWhiteSpace(parameters.search) || p.Name.ToLower().Contains(parameters.search.Trim().ToLower()));
        }

        private void ApplySorting(SortingOptions sorting)
        {
            switch (sorting)
            {
                case SortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case SortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case SortingOptions.CreatedAtAsc:
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case SortingOptions.CreatedAtDesc:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}
