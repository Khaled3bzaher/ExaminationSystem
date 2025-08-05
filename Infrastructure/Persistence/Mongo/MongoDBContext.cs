using Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Mongo.Settings;

namespace Persistence.Mongo
{
    public class MongoDBContext
    {
        private readonly IMongoCollection<Notification> _notificationCollection;
        public MongoDBContext(IOptions<MongoDBSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
            _notificationCollection = database.GetCollection<Notification>(options.Value.CollectionName);
        }

        public IMongoCollection<Notification> NotificationCollection => _notificationCollection;
    }
}
