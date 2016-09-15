namespace WarmBeer.Core.Domain
{
    public class StoreItem
    {
        public int Id { get; private set; }

        public Store Store { get; set; }

        public Item Item { get; set; }
    }
}
