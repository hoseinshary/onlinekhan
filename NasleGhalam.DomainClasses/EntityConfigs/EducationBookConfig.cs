using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationBookConfig : EntityTypeConfiguration<EducationBook>
    {
        public EducationBookConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(200).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_EducationBook_Name");

            this.HasRequired(x => x.GradeLevel)
                .WithMany(x => x.EducationBooks)
                .HasForeignKey(x => x.GradeLevelId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.EducationGroup_Lesson)
                .WithMany(x => x.EducationBooks)
                .HasForeignKey(x => x.EducationGroup_LessonId)
                .WillCascadeOnDelete(false);
        }
    }
}
