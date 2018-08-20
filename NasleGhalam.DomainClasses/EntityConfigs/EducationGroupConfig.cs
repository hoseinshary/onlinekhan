using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationGroupConfig : EntityTypeConfiguration<EducationGroup>
    {
        public EducationGroupConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50);
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_EducationGroup_Name");
        }
    }
}
