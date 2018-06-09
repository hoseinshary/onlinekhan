using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class RatioConfig : EntityTypeConfiguration<Ratio>
    {
        public RatioConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(200).IsRequired();

            this.HasRequired(x => x.EducationSubGroup)
                .WithMany(x => x.Ratios)
                .HasForeignKey(x => x.EducationSubGroupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lesson)
                .WithMany(x => x.Ratios)
                .HasForeignKey(x => x.LessonId)
                .WillCascadeOnDelete(false);
        }
    }
}
