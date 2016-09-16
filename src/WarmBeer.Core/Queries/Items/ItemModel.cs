namespace WarmBeer.Core.Queries.Items
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string ItemNumber { get; set; }

        public string Name { get; set; }

        public decimal Volume { get; set; }

        public decimal AlcoholByVolume { get; set; }

        public decimal Price { get; set; }

        public decimal PricePerLitre { get; set; }

        public bool IsKoscher { get; set; }

        public bool IsOrganic { get; set; }

        public string ImageUrl => $"http://static.systembolaget.se/imagelibrary/publishedmedia/ph2255i3v8j29mt4lqn2/{ItemNumber}.jpg";
    }
}
