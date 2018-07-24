using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class GradeConfig : EntityTypeConfiguration<Grade>
    {
        public GradeConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_Grade_Name");
            
        }
    }
}
