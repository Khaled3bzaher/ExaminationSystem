using Shared.DTOs.Notifications;

namespace Services.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDTO, Notification>();
        }
    }
}
