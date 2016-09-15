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
        }

        [Fact]
        public void Run()
        {
            using (var db = new WarmBeerDbContext())
            {
                var storeLoader = new StoreLoader(db, @"C:\@github\warmbeer\data");
                storeLoader.Run();
            }
        }
    }
}
