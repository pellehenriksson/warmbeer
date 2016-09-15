﻿using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Queries.Items
{
    public class ItemQuery : IAsyncRequestHandler<ItemQuery.Parameters, ItemModel>
    {
        private readonly WarmBeerDbContext db;

        public ItemQuery(WarmBeerDbContext db)
        {
            this.db = db;
        }

        public async Task<ItemModel> Handle(Parameters message)
        {
            var result = await this.db.Items.Where(x => x.Id == message.Id)
                                        .Select(x => new ItemModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name
                                        }).FirstOrDefaultAsync();

            return result;
        }

        public class Parameters : IAsyncRequest<ItemModel>
        {
            public Parameters(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }
    }
}