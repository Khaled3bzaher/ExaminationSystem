using Shared.Options.SortingOptions;

namespace Shared.QueryParameters
{
    public class QuestionQueryParameters : BaseParameters
    {
        public Guid? SubjectId { get; set; }
        public QuestionsSortOptions sorting { get; set; } = QuestionsSortOptions.CreatedAtAsc;
       
    }
}
