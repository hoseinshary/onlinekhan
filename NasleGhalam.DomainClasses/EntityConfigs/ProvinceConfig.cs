using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ProvinceConfig : EntityTypeConfiguration<Province>
    {
        public ProvinceConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_Province_Name");
            this.Property(x => x.Code).HasMaxLength(5).IsRequired();
        }
    }
}
