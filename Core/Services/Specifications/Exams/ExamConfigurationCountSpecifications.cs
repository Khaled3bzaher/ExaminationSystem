using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class ExamConfigurationCountSpecifications : BaseSpecifications<ExamConfiguration>
    {
        public ExamConfigurationCountSpecifications(SubjectConfigurationQueryParameters parameters) : base(CreateCriteria(parameters))
        {
        }
        private static Expression<Func<ExamConfiguration, bool>> CreateCriteria(SubjectConfigurationQueryParameters parameters)
        {
            return p =>
       (string.IsNullOrWhiteSpace(parameters.search) || p.Subject.Name.ToLower().Contains(parameters.search.Trim().ToLower()));
        }
    }
}
