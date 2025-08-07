namespace Shared.DTOs.Exams
{
    public class ExamHistoryResponse
    {
        public string Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public DateTime ExamDateTime { get; set; }
        public string ExamStatus { get; set; }
        public double Result { get; set; }
    }
}
