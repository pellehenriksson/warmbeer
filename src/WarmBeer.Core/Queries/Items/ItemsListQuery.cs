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
                .Select(x => new ItemsModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                .ToListAsync();

            var result = new Result(message, totalCount, items);

            return result;
        }

        public class Parameters : PagedParameters, IAsyncRequest<Result>
        {
        }
        
        public class Result : PagedResult<ItemsModel>
        {
            public Result(PagedParameters parameters, int totalCount, List<ItemsModel> items) : base(parameters, totalCount, items)
            {
            }
        }
    }
}
