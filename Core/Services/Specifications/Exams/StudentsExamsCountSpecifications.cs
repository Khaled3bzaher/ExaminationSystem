using System.Linq.Expressions;

namespace Services.Specifications.Exams
{
    internal class StudentsExamsCountSpecifications(ExamHistoryParameters parameters) : BaseSpecifications<StudentExam>(CreateCriteria(parameters))
    {
        private static Expression<Func<StudentExam, bool>> CreateCriteria(ExamHistoryParameters parameters)
        {
            return p =>
       (string.IsNullOrWhiteSpace(parameters.search) || p.Subject.Name.ToLower().Contains(parameters.search.Trim().ToLower())) &&
       (string.IsNullOrWhiteSpace(parameters.studentId) || p.StudentId == parameters.studentId);
        }
    }
}
