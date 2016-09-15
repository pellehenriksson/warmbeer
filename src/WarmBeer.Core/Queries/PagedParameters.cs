namespace WarmBeer.Core.Queries
{
    public abstract class PagedParameters
    {
        protected PagedParameters()
        {
            this.Page = 1;
            this.Size = 50;
        }

        public int Page { get; set; }

        public int Size { get; set; }
    }
}
