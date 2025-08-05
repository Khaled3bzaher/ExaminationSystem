using Domain.Contracts;
using Domain.Services;
using MongoDB.Driver;
using Services.Specifications.Exams;
using Services.Specifications.Notifications;
using Shared.DTOs.Exams;
using Shared.DTOs.Notifications;

namespace Services.Repositories
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateNotification(NotificationDTO notification)
        {
            var Notification = _mapper.Map<Notification>(notification);
            await _repository.CreateAsync(Notification);
        }

        public async Task<APIResponse<PaginatedResponse<NotificationResponse>>> GetAllNotifications(NotificationParameters parameters)
        {
            var projection = Builders<Notification>.Projection.Expression(n => new NotificationResponse
            {
                Title = n.Title,
                Message = n.Message,
                CreatedAt = n.CreatedAt,
                Id = n.Id,
            });
            var specificationsCount = new NotificationsCountSpecifications(parameters);
            var notificationsCount = await _repository.GetNotificationsCountAsync(specificationsCount, projection);
            if(notificationsCount == 0)
                return APIResponse<PaginatedResponse<NotificationResponse>>.FailureResponse($"No notifications found for the given parameters.", (int)HttpStatusCode.NotFound);

            var specifications = new NotificationsSpecifications(parameters);

            var notifications = await _repository.GetAllProjectedNotificationsAsync(specifications, projection);
            var pageCount = notifications.Count();
            var paginatedData = new PaginatedResponse<NotificationResponse>(parameters.PageIndex, pageCount, (int)notificationsCount, notifications);
            return APIResponse<PaginatedResponse<NotificationResponse>>.SuccessResponse(paginatedData);
        }

        public async Task<APIResponse<string>> MarkNotificationAsRead(string notificationId)
        {

            try
            {
                await _repository.MarkAsRead(notificationId);
                return APIResponse<string>.SuccessResponse(null, "Notification Marked As Read");
            }
            catch (Exception ex)
            {
                return APIResponse<string>.FailureResponse("Failed to mark notification as read");
            }
        }
    }
}
