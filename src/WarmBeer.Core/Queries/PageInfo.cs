namespace WarmBeer.Core.Queries
{
    public class PageInfo
    {
        public int TotalCount { get; set; }

        public int ItemCount { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
