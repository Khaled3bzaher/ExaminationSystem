using Shared.Options.SortingOptions;
using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class StudentsExamsSpecifications : BaseSpecifications<StudentExam>
    {
        public StudentsExamsSpecifications(string studentId,Guid subjectId) : base(s=>s.StudentId==studentId && s.SubjectId==subjectId)
        {
        }
        public StudentsExamsSpecifications(ExamHistoryParameters parameters) : base(CreateCriteria(parameters))
        {
            AddInclude(p => p.Student);
            AddInclude(p => p.Subject);
            ApplySorting(parameters.sorting);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }

        private static Expression<Func<StudentExam, bool>> CreateCriteria(ExamHistoryParameters parameters)
        {
            return p =>
       (string.IsNullOrWhiteSpace(parameters.search) || p.Subject.Name.ToLower().Contains(parameters.search.Trim().ToLower())) &&
       (string.IsNullOrWhiteSpace(parameters.studentId) || p.StudentId == parameters.studentId);
        }

        private void ApplySorting(ExamHistorySorting sorting)
        {
            switch (sorting)
            {
                case ExamHistorySorting.NameAsc:
                    AddOrderBy(p => p.Student.FullName);
                    break;
                case ExamHistorySorting.NameDesc:
                    AddOrderByDescending(p => p.Student.FullName);
                    break;
                case ExamHistorySorting.CreatedAtAsc:
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case ExamHistorySorting.CreatedAtDesc:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                case ExamHistorySorting.StatusAsc:
                    AddOrderBy(p => p.ExamStatus);
                    break;
                case ExamHistorySorting.StatusDesc:
                    AddOrderByDescending(p => p.ExamStatus);
                    break;
                default:
                    AddOrderBy(p => p.Student.FullName);
                    break;
            }
        }
    }
}
