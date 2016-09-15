using System.Data.Entity;
using WarmBeer.Core.Import;
using WarmBeer.Core.Infrastructure.Persistence;
using Xunit;

namespace WarmBeer.Tests
{
    public class Sandbox
    {
        public Sandbox()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WarmBeerDbContext>());

            using (var db = new WarmBeerDbContext())
            {
                db.Database.ExecuteSqlCommand("delete from StoreItems; delete from Stores; delete from Items");
            }
        }

        [Fact]
        public void Run()
        {
            using (var db = new WarmBeerDbContext())
            {
                var storeLoader = new StoreLoader(db, @"C:\@github\warmbeer\data");
                storeLoader.Run();

                var itemLoader = new ItemLoader(db, @"C:\@github\warmbeer\data");
                itemLoader.Run();
            }
        }
    }
}
