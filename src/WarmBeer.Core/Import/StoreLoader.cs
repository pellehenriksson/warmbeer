using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using MightyLittleGeodesy.Positions;
using WarmBeer.Core.Domain;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Import
{
    public class StoreLoader
    {
        private readonly WarmBeerDbContext db;

        private readonly string directory;

        public StoreLoader(WarmBeerDbContext db, string directory)
        {
            this.db = db;
            this.directory = directory;
        }

        public void Run()
        {
            var path = Path.Combine(this.directory, "stores.xml");

            var data = File.ReadAllText(path);
            var doc = XDocument.Load(new StringReader(data));

            foreach (XElement s in doc.Root.Elements("ButikOmbud"))
            {
                var type = s.Element("Typ").Value.Trim();

                if (type.Equals("Butik"))
                {
                    this.db.Stores.Add(new Store
                    {
                        Name = s.Element("Namn").Value,
                        Phone = this.ResolvePhone(s.Element("Telefon")),
                        Address = new Address
                        {
                            Street = s.Element("Address1") != null ? s.Element("Address1").Value.Trim() : string.Empty,
                            Postalcode = s.Element("Address3") != null ? s.Element("Address3").Value.Trim() : string.Empty,
                            City = s.Element("Address4") != null ? s.Element("Address4").Value.Trim() : string.Empty,
                            County = s.Element("Address5") != null ? s.Element("Address5").Value.Trim() : string.Empty
                        },
                        Location = this.ResolveLocation(s)
                    });
                }
            }
            
            this.db.SaveChanges();
        }

        private Phone ResolvePhone(XElement element)
        {
            if (element == null || string.IsNullOrWhiteSpace(element.Value))
            {
                return new Phone();
            }

            // find areacode
            var separatorIndex = element.Value.LastIndexOf('/');
            if (separatorIndex != -1)
            {
                return new Phone
                {
                    Areacode = element.Value.Substring(0, separatorIndex).Replace(" ", string.Empty),
                    Number = element.Value.Substring(separatorIndex + 1).Replace(" ", string.Empty)
                };
            }

            separatorIndex = element.Value.LastIndexOf('-');
            if (separatorIndex != -1)
            {
                return new Phone
                {
                    Areacode = element.Value.Substring(0, separatorIndex).Replace(" ", string.Empty),
                    Number = element.Value.Substring(separatorIndex + 1).Replace(" ", string.Empty)
                };
            }

            return new Phone();
        }

        private DbGeography ResolveLocation(XElement e)
        {
            //// convert swedish rt90 to WGS84

            var rt90X = !string.IsNullOrWhiteSpace(e.Element("RT90x").Value) ? double.Parse(e.Element("RT90x").Value, CultureInfo.InvariantCulture) : 0;
            var rt90Y = !string.IsNullOrWhiteSpace(e.Element("RT90y").Value) ? double.Parse(e.Element("RT90y").Value, CultureInfo.InvariantCulture) : 0;

            if (rt90X == 0 || rt90Y == 0)
            {
                return null;
            }

            var rt90Position = new RT90Position(rt90X, rt90Y);
            var wgs84Position = rt90Position.ToWGS84();

            var longitude = wgs84Position.LongitudeToString(WGS84Position.WGS84Format.Degrees).Replace(",", ".");
            var latitude = wgs84Position.LatitudeToString(WGS84Position.WGS84Format.Degrees).Replace(",", ".");

            var pointString = string.Format("POINT({0} {1})", longitude, latitude);

            return DbGeography.FromText(pointString);
        }
    }
}
