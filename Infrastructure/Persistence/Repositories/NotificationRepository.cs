using Domain.Contracts;
using Domain.Models;
using MongoDB.Driver;
using Persistence.Mongo;

namespace Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _collection;
        public NotificationRepository(MongoDBContext context)
        {
            _collection = context.NotificationCollection;
        }
        public async Task CreateAsync(Notification notification)
        {
            await _collection.InsertOneAsync(notification);
        }

        public async Task<IEnumerable<TDto>> GetAllProjectedNotificationsAsync<TDto>(ISpecifications<Notification> specifications, ProjectionDefinition<Notification, TDto> projection) 
        {

            return await SpecificationsEvaluator.GetMongoQuery<Notification, TDto>(_collection, specifications,projection).ToListAsync();
        }
        public async Task<long> GetNotificationsCountAsync<TDto>(ISpecifications<Notification> specifications, ProjectionDefinition<Notification, TDto> projection)
        {
            return await SpecificationsEvaluator.GetMongoQuery<Notification, TDto>(_collection, specifications, projection).CountDocumentsAsync();
        }

        public async Task MarkAsRead(string notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.ViewedAt, DateTime.Now);
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
