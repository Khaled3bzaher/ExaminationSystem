namespace Shared.DTOs.Admin
{
    public class StatsResponse
    {
        public int StudentsNumber { get; set; }
        public int ExamCompleted { get; set; }
        public int ExamPassed { get; set; }
        public int ExamFailed { get; set; }

    }
}
