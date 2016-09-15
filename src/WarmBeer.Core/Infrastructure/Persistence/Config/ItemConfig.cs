using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class ItemConfig : EntityTypeConfiguration<Item>
    {
        public ItemConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.Number)
                .HasMaxLength(10)
                .IsRequired();

            this.Property(x => x.Name)
             .HasMaxLength(200)
             .IsRequired();

            this.Property(x => x.Volume);
            this.Property(x => x.Price);
            this.Property(x => x.PricePerLitre);
            this.Property(x => x.AlcoholByVolume);

            this.HasMany(x => x.Stores);
        }
    }
}
