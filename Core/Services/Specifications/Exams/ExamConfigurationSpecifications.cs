using Shared.Options.SortingOptions;
using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class ExamConfigurationSpecifications : BaseSpecifications<ExamConfiguration>
    {
        public ExamConfigurationSpecifications(Guid subjectId) : base(e=>e.SubjectId == subjectId)
        {
        }
        public ExamConfigurationSpecifications(
            SubjectConfigurationQueryParameters parameters) : base(CreateCriteria(parameters))
            {
            ApplySorting(parameters.sorting);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
            }

        private static Expression<Func<ExamConfiguration, bool>> CreateCriteria(SubjectConfigurationQueryParameters parameters)
        {
            return p =>
       (string.IsNullOrWhiteSpace(parameters.search) || p.Subject.Name.ToLower().Contains(parameters.search.Trim().ToLower()));
        }

        private void ApplySorting(ConfigurationSortOptions sorting)
        {
            switch (sorting)
            {
                case ConfigurationSortOptions.NameAsc:
                    AddOrderBy(p => p.Subject.Name);
                    break;
                case ConfigurationSortOptions.NameDesc:
                    AddOrderByDescending(p => p.Subject.Name);
                    break;
                case ConfigurationSortOptions.NumberOfQuestionsAsc:
                    AddOrderBy(p => p.QuestionNumbers);
                    break;
                case ConfigurationSortOptions.NumberOfQuestionsDesc:
                    AddOrderByDescending(p => p.QuestionNumbers);
                    break;
                case ConfigurationSortOptions.DurationAsc:
                    AddOrderBy(p => p.DurationInMinutes);
                    break;
                case ConfigurationSortOptions.DurationDesc:
                    AddOrderByDescending(p => p.DurationInMinutes);
                    break;
                case ConfigurationSortOptions.LowPercentageAsc:
                    AddOrderBy(p => p.LowPercentage);
                    break;
                case ConfigurationSortOptions.LowPercentageDesc:
                    AddOrderByDescending(p => p.LowPercentage);
                    break;
                case ConfigurationSortOptions.NormalPercentageAsc:
                    AddOrderBy(p => p.NormalPercentage);
                    break;
                case ConfigurationSortOptions.NormalPercentageDesc:
                    AddOrderByDescending(p => p.NormalPercentage);
                    break;
                case ConfigurationSortOptions.HardPercentageAsc:
                    AddOrderBy(p => p.HardPercentage);
                    break;
                case ConfigurationSortOptions.HardPercentageDesc:
                    AddOrderByDescending(p => p.HardPercentage);
                    break;
                default:
                    AddOrderBy(p => p.Subject.Name);
                    break;
            }
        }
    }
}
