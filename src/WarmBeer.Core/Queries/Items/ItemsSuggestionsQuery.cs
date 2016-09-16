using System;
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
                .Take(5)
                .Select(x => new Result.Store
                    {
                        Id = x.Id,
                        Address = x.Address.Street,
                        City = x.Address.City,
                        Location = x.Location.AsText(),
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude,
                        Distance = x.Location.Distance(location)
                    })
                .ToListAsync();

            var storeIds = stores.Select(x => x.Id).ToList();
            
            var itemsQuery = this.db.StoreItems
                                .Where(x => storeIds.Contains(x.Store.Id))
                                .GroupBy(x => x.Item.Id)
                                .Select(grp => grp.FirstOrDefault());

            if (message.Setting == Parameters.ResultSetting.Strongest)
            {
                itemsQuery = itemsQuery
                    .OrderByDescending(x => x.Item.AlcoholByVolume);
            }
            else if (message.Setting == Parameters.ResultSetting.LowestPrice)
            {
                itemsQuery = itemsQuery.OrderBy(x => x.Item.PricePerLitre);
            }
            else
            {
                itemsQuery = itemsQuery.OrderByDescending(x => x.Item.PricePerLitre);
            }

            var items = await itemsQuery
                            .Take(10)
                            .Select(x => new ItemModel
                                {
                                    Id = x.Item.Id,
                                    ItemNumber = x.Item.ItemNumber,
                                    Name = x.Item.Name,
                                    Price = x.Item.Price,
                                    PricePerLitre = x.Item.PricePerLitre,
                                    AlcoholByVolume = x.Item.AlcoholByVolume,
                                    IsKoscher = x.Item.IsKoscher,
                                    IsOrganic = x.Item.IsOrganic,
                                    Address = x.Store.Address.Street
                                })
                        .ToListAsync();

            return new Result { Stores = stores, Items = items };
        }
        
        public class Parameters : IAsyncRequest<Result>
        {
            public decimal Longitude { get; set; }

            public decimal Latitude { get; set; }

            public int Radius { get; set; }

            public ResultSetting Setting { get; set; }

            public enum ResultSetting
            {
                LowestPrice = 1,
                Strongest = 2, 
                HighestPrice = 3
            }
        }

        public class Result 
        {
            public List<Store> Stores { get; set; }

            public List<ItemModel> Items { get; set; }

            public class Store
            {
                public int Id { get; set; }

                public string Address { get; set; }

                public string City { get; set; }

                public string Location { get; set; }

                public double?  Latitude { get; set; }

                public double? Longitude { get; set; }

                public double? Distance { get; set; }

                public string Info
                {
                    get
                    {
                        if (!this.Distance.HasValue)
                        {
                            return this.Address;
                        }

                        var dist = Math.Round(this.Distance.Value/1000, 1);
                        return $"{this.Address} Distance: {dist} Km";
                    }
                }
            }
        }
    }
}
