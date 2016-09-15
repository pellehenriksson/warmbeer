using System.Collections.Generic;

namespace WarmBeer.Core.Domain
{
    public class Item
    {
        public int Id { get; private set; }

        public string Number { get; set; } // public number

        public string Name { get; set; }

        public decimal Volume { get; set; }

        public decimal AlcoholByVolume { get; set; }

        public decimal Price { get; set; }

        public decimal PricePerLitre { get; set; }

        public bool IsKoscher { get; set; }

        public bool IsOrganic { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<StoreItem> Stores { get; set; }
    }
}
