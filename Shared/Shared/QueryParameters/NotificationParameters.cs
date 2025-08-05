namespace Shared.QueryParameters
{
    public class NotificationParameters : BaseParameters
    {
        public string? studentId { get; set; }
        public bool ForAdmin { get; set; } = false;
    }
}
