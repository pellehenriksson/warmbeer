using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Queries.Items
{
    public class ItemsListQuery : IAsyncRequestHandler<ItemsListQuery.Parameters, ItemsListQuery.Result>
    {
        private readonly WarmBeerDbContext db;

        public ItemsListQuery(WarmBeerDbContext db)
        {
            this.db = db;
        }

        public async Task<ItemsListQuery.Result> Handle(Parameters message)
        {
            var totalCount = await this.db.Items.CountAsync();

            var items = await this.db.Items
                .OrderBy(x => x.Number)
                .Skip((message.Page - 1) * message.Size)
                .Take(message.Size)
                .Select(x => new ItemModel
                    {
                        Id = x.Id,
                        ItemNumber = x.ItemNumber,
                        Name = x.Name,
                        Price = x.Price,
                        PricePerLitre = x.PricePerLitre,
                        AlcoholByVolume = x.AlcoholByVolume,
                        IsKoscher = x.IsKoscher,
                        IsOrganic = x.IsOrganic
                    })
                .ToListAsync();

            var result = new Result(message, totalCount, items);

            return result;
        }

        public class Parameters : PagedParameters, IAsyncRequest<Result>
        {
        }
        
        public class Result : PagedResult<ItemModel>
        {
            public Result(PagedParameters parameters, int totalCount, List<ItemModel> items) : base(parameters, totalCount, items)
            {
            }
        }
    }
}
