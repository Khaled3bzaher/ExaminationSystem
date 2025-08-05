
using Shared.DTOs;
using Shared.DTOs.Notifications;
using Shared.QueryParameters;

namespace Domain.Services
{
    public interface INotificationService
    {
        Task CreateNotification(NotificationDTO notification);
        Task<APIResponse<PaginatedResponse<NotificationResponse>>> GetAllNotifications(NotificationParameters parameters);
        Task<APIResponse<string>> MarkNotificationAsRead(string notificationId);
    }
}
