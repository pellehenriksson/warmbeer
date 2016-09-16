using System.IO;
using System.Xml.Linq;
using WarmBeer.Core.Domain;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Core.Import
{
    public class ItemLoader
    {
        private readonly WarmBeerDbContext db;

        private readonly string directory;

        public ItemLoader(WarmBeerDbContext db, string directory)
        {
            this.db = db;
            this.directory = directory;
        }

        public void Run()
        {
            var path = Path.Combine(this.directory, "items.xml");

            var data = File.ReadAllText(path);
            var doc = XDocument.Load(new StringReader(data));

            foreach (XElement s in doc.Root.Elements("artikel"))
            {
                if (s.Element("Varugrupp") == null)
                {
                    continue;
                }

                var group = s.Element("Varugrupp").Value;

                if (!group.ToLowerInvariant().Contains("öl"))
                {
                    continue;
                }

                var product = new Item
                {
                    Number = s.Element("nr").Value,
                    ItemNumber = s.Element("Artikelid").Value,
                    Name = this.ResolveName(s),
                    Price = this.ParseDecimal(s.Element("Prisinklmoms")),
                    Volume = this.ParseDecimal(s.Element("Volymiml")),
                    PricePerLitre = this.ParseDecimal(s.Element("PrisPerLiter")),
                    AlcoholByVolume = s.Element("Alkoholhalt") == null ? 0 : decimal.Parse(s.Element("Alkoholhalt").Value.Replace("%", string.Empty).Replace(".", ",")),
                    IsOrganic = s.Element("Ekologisk").Value == "0" ? false : true,
                    IsKoscher = s.Element("Koscher").Value == "0" ? false : true
                };

                this.db.Items.Add(product);
                this.db.SaveChanges();
            }
        }

        private string ResolveName(XElement e)
        {
            var name = e.Element("Namn").Value;

            if (e.Element("Namn2") != null)
            {
                var name2 = e.Element("Namn2").Value;
                return string.Format("{0} {1}", name, name2);
            }

            return name;
        }

        private decimal ParseDecimal(XElement e)
        {
            if (e == null)
            {
                return 0;
            }

            var amount = decimal.Parse(e.Value.Replace('.', ','));
            return amount;
        }
    }
}
