using Shared.Options.SortingOptions;

namespace Shared.QueryParameters
{
    public class ExamHistoryParameters : BaseParameters
    {
        public string? studentId { get; set; }
        public ExamHistorySorting sorting { get; set; } = ExamHistorySorting.CreatedAtAsc;
    }
}
