using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace WarmBeer.Core.Domain
{
    public class Store
    {
        public int Id { get; private set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public DbGeography Location { get; set; }

        public virtual ICollection<StoreItem> Items { get; set; }
    }
}
