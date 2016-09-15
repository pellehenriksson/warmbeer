namespace WarmBeer.Web.Models
{
    public class PagingModel
    {
        public const int DefaultPage = 1;

        public const int DefaultSize = 50;

        private int page;

        private int size;

        public PagingModel()
        {
            // default values
            this.Page = DefaultPage;
            this.Size = DefaultSize;
        }

        public int Page
        {
            get { return this.page; }
            set { this.page = value < 1 ? DefaultPage : value; }
        }

        public int Size
        {
            get { return this.size; }
            set { this.size = (value < 1 || value > 128) ? DefaultSize : value; }
        }
    }
}