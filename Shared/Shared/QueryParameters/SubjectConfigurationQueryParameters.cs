using Shared.Options.SortingOptions;

namespace Shared.QueryParameters
{
    public class SubjectConfigurationQueryParameters : BaseParameters
    {
        public ConfigurationSortOptions sorting { get; set; } = ConfigurationSortOptions.NameAsc;
    }
}
