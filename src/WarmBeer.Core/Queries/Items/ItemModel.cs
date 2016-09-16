namespace WarmBeer.Core.Queries.Items
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Volume { get; set; }

        public decimal AlcoholByVolume { get; set; }

        public decimal Price { get; set; }

        public decimal PricePerLitre { get; set; }

        public bool IsKoscher { get; set; }

        public bool IsOrganic { get; set; }

        public string Address { get; set; }

        public string Url => $"http://www.systembolaget.se/sok-dryck?searchquery={this.Number}";
    }
}
