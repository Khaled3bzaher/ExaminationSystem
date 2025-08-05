using Domain.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface INotificationRepository
    {
        Task CreateAsync(Notification notification);
        Task<IEnumerable<TDto>> GetAllProjectedNotificationsAsync<TDto>(ISpecifications<Notification> specifications, ProjectionDefinition<Notification, TDto> projection);
        Task<long> GetNotificationsCountAsync<TDto>(ISpecifications<Notification> specifications, ProjectionDefinition<Notification, TDto> projection);

        Task MarkAsRead(string notificationId);

    }
}
