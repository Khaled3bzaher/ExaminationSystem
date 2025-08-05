namespace Shared.DTOs.Notifications
{
    public class NotificationResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        //public DateTime? ViewedAt { get; set; }
    }
}
