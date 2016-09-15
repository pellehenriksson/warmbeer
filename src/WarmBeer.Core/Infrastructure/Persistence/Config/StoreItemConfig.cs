using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class StoreItemConfig : EntityTypeConfiguration<StoreItem>
    {
        public StoreItemConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(x => x.Store);
            this.HasRequired(x => x.Item);
        }
    }
}
