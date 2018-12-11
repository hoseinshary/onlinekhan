using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class GradeLevelConfig : EntityTypeConfiguration<GradeLevel>
    {
        public GradeLevelConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();

            this.HasRequired(x => x.Grade)
                .WithMany(x => x.GradeLevels)
                .HasForeignKey(x => x.GradeId)
                .WillCascadeOnDelete(false);
        }
    }
}
