namespace Shared.DTOs.Subjects
{
    public class SubjectConfigurationResponse
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int QuestionNumbers { get; set; }
        public int HardPercentage { get; set; }
        public int DurationInMinutes { get; set; }
        public int NormalPercentage { get; set; }
        public int LowPercentage { get; set; }
    }
}
