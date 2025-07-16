namespace Shared.QueryParameters
{
    public class BaseParameters
    {
        private const int DEFAULT_PAGE_SIZE = 5;
        private const int MAX_PAGE_SIZE = 10;
        public string? search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = DEFAULT_PAGE_SIZE;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 && value < MAX_PAGE_SIZE ? value : DEFAULT_PAGE_SIZE;
        }
    }
}
