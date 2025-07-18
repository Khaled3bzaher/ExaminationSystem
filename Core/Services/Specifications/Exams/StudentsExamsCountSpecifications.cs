using Domain.Enums;
using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class StudentsExamsCountSpecifications : BaseSpecifications<StudentExam>
    {
        public StudentsExamsCountSpecifications(ExamHistoryParameters parameters) : base(CreateCriteria(parameters))
        {
        }
        public StudentsExamsCountSpecifications(ExamStatus examStatus) : base(e=>e.ExamStatus==examStatus)
        {
        }

        private static Expression<Func<StudentExam, bool>> CreateCriteria(ExamHistoryParameters parameters)
        {
            return p =>
       (string.IsNullOrWhiteSpace(parameters.search) || p.Subject.Name.ToLower().Contains(parameters.search.Trim().ToLower())) &&
       (string.IsNullOrWhiteSpace(parameters.studentId) || p.StudentId == parameters.studentId);
        }
    }
}
