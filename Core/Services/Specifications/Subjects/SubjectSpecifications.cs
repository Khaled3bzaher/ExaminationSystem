﻿using Shared.Options.SortingOptions;
using Shared.QueryParameters;
using System.Linq.Expressions;

namespace Services.Specifications.Subjects
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
            return p => string.IsNullOrWhiteSpace(parameters.search) || p.Name.ToLower().Contains(parameters.search.Trim().ToLower());
        }

        private void ApplySorting(BaseSortingOptions sorting)
        {
            switch (sorting)
            {
                case BaseSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case BaseSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case BaseSortingOptions.CreatedAtAsc:
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case BaseSortingOptions.CreatedAtDesc:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}
