using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ControllerConfig : EntityTypeConfiguration<Controller>
    {
        public ControllerConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.FaName).HasMaxLength(50).IsRequired();
            this.Property(x => x.EnName).HasMaxLength(50).IsRequired();
            this.Property(x => x.Icone).HasMaxLength(200).IsRequired();
        }
    }
}
