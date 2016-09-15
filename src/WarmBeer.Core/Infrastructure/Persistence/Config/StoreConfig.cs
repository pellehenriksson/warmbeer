using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class StoreConfig : EntityTypeConfiguration<Store>
    {
        public StoreConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(x => x.Name)
                .HasMaxLength(50);
            
            this.Property(x => x.Location);
            this.HasMany(x => x.Items);
        }
    }
}
