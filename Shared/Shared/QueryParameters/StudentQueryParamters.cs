using Shared.Options.SortingOptions;

namespace Shared.QueryParameters
{
    public class StudentQueryParamters : BaseParameters
    {
        public StudentSortOptions sorting { get; set; } = StudentSortOptions.NameAsc;
        
    }
}
