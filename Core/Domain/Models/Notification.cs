using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? ViewedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
