namespace Shared.DTOs.Students
{
    public class StudentResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
