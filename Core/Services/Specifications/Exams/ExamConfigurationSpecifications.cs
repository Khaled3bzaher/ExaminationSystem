using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class ExamConfigurationSpecifications : BaseSpecifications<ExamConfiguration>
    {
        public ExamConfigurationSpecifications(Guid subjectId) : base(e=>e.SubjectId == subjectId)
        {
        }
    }
}
