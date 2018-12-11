using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_Role_Name");
            this.Property(x => x.SumOfActionBit).HasMaxLength(300).IsRequired();
        }
    }
}
