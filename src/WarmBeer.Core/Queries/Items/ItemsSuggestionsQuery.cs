using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Queries.Items
{
    public class ItemsSuggestionsQuery : IAsyncRequestHandler<ItemsSuggestionsQuery.Parameters, ItemsSuggestionsQuery.Result>
    {
        private readonly WarmBeerDbContext db;

        public ItemsSuggestionsQuery(WarmBeerDbContext db)
        {
            this.db = db;
        }

        public async Task<Result> Handle(Parameters message)
        {
            var point = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", message.Longitude, message.Latitude);
            var location = DbGeography.FromText(point);

            var stores = await this.db.Stores.Where(x => x.Location.Distance(location) < message.Radius)
                .OrderBy(x => x.Location.Distance(location))
                .Select(x => new Result.Store
                    {
                        Id = x.Id,
                        Address = x.Address.Street,
                        City = x.Address.City,
                        Location = x.Location.AsText(),
                        Distance = x.Location.Distance(location)
                    })
                .ToListAsync();

            var storeIds = stores.Select(x => x.Id).ToList();


            var itemsQuery =  this.db.StoreItems
                                    .Where(x => storeIds.Contains(x.Store.Id));

            if (message.HighestAlcohol)
            {                itemsQuery = itemsQuery.OrderByDescending(x => x.Item.AlcoholByVolume);
            }
            else
            {
                itemsQuery = itemsQuery.OrderBy(x => x.Item.PricePerLitre);
            }

            var items = await itemsQuery
                            .Take(10)
                                .Select(x => new Result.Item
                                {
                                    Id = x.Item.Id,
                                    Name = x.Item.Name,
                                    Price = x.Item.Price,
                                    PricePerLitre = x.Item.PricePerLitre,
                                    Alcohol = x.Item.AlcoholByVolume
                                })
                            .ToListAsync();

            return new Result { Stores = stores, Items = items };
        }
        
        public class Parameters : IAsyncRequest<Result>
        {
            public decimal Longitude { get; set; }

            public decimal Latitude { get; set; }

            // meter
            public int Radius { get; set; }

            public bool HighestAlcohol { get; set; }
        }

        public class Result 
        {
            public List<Store> Stores { get; set; }

            public List<Item> Items { get; set; }

            public class Store
            {
                public int Id { get; set; }

                public string Address { get; set; }

                public string City { get; set; }

                public string Location { get; set; }

                // meter
                public double? Distance { get; set; }
            }
            
            public class Item
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public decimal Price { get; set; }

                public decimal PricePerLitre { get; set; }

                public decimal Alcohol { get; set; }
            }
        }
    }
}
