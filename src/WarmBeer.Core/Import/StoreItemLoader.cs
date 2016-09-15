using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Import
{
    public class StoreItemLoader
    {
        private readonly WarmBeerDbContext db;

        private readonly string directory;

        public StoreItemLoader(WarmBeerDbContext db, string directory)
        {
            this.db = db;
            this.directory = directory;
        }
        
        public void Run()
        {
            var path = Path.Combine(this.directory, "storeitems.xml");

            var data = File.ReadAllText(path);
            var doc = XDocument.Load(new StringReader(data));

            using (var connection = new SqlConnection(this.db.Database.Connection.ConnectionString))
            {
                connection.Open();

                foreach (XElement s in doc.Root.Elements("Butik"))
                {
                    var storeNumber = s.Attribute("ButikNr").Value;
                    var itemNumbers = s.Elements("ArtikelNr").Select(x => x.Value).ToList();

                    foreach (var itemNumber in itemNumbers)
                    {
                        using (var command = new SqlCommand(
@"if (exists(select * from Items where Number = @itemNumber) and exists(select * from Stores where Number = @storeNumber))
begin
	declare @pId int
	declare @sId int

	select @pId = Id from Items where Number = @itemNumber
	select @sId = Id from Stores where Number = @storeNumber
	insert into StoreItems (Item_Id, Store_Id) values (@pId, @sId)
end",
connection))
                        {
                            command.Parameters.AddWithValue("@storeNumber", storeNumber);
                            command.Parameters.AddWithValue("@itemNumber", itemNumber);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}