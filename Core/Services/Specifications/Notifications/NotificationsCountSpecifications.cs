using System.Linq.Expressions;

namespace Services.Specifications.Notifications
{
    internal class NotificationsCountSpecifications : BaseSpecifications<Notification>
    {
        public NotificationsCountSpecifications(NotificationParameters parameters) : base(CreateCriteria(parameters))
        {
        }
        private static Expression<Func<Notification, bool>> CreateCriteria(NotificationParameters parameters)
        {
            return p =>
        (string.IsNullOrWhiteSpace(parameters.search) || p.Message.ToLower().Contains(parameters.search.Trim().ToLower())) &&
        (string.IsNullOrWhiteSpace(parameters.studentId) || p.StudentId == parameters.studentId) &&
        (
            parameters.ForAdmin || p.ViewedAt == null
        );
        }
    }
}
