using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class AddressConfiguration : ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            this.Property(x => x.Street)
                .HasMaxLength(50);
            
            this.Property(x => x.Postalcode)
                .HasMaxLength(10);
            
            this.Property(x => x.City)
                .HasMaxLength(50);

            this.Property(x => x.County)
                .HasMaxLength(50);
        }
    }
}
