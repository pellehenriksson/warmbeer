using System.Data.Entity;
using System.Linq;
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
                var x = db.Items.ToList();
            }
        }
    }
}
