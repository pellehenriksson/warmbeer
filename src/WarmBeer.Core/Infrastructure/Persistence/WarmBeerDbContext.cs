using System.Data.Entity;
using WarmBeer.Core.Domain;
using WarmBeer.Core.Infrastructure.Persistence.Config;

namespace WarmBeer.Core.Infrastructure.Persistence
{
    public class WarmBeerDbContext : DbContext
    {
        public WarmBeerDbContext() : base("warm-beer")
        {
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<StoreItem> StoreItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ItemConfig).Assembly);
        }
    }
}
