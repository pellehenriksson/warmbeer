using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class PhoneConfiguration : ComplexTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            this.Property(x => x.Areacode)
                .HasMaxLength(5);

            this.Property(x => x.Number)
                .HasMaxLength(20);
        }
    }
}
