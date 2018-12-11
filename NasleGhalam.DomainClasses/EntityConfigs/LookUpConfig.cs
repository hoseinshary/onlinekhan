using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LookupConfig : EntityTypeConfiguration<Lookup>
    {
        public LookupConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.Property(x => x.Value).HasMaxLength(50).IsRequired();
        }
    }
}