using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WarmBeer.Core.Domain;

namespace WarmBeer.Core.Infrastructure.Persistence.Config
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
