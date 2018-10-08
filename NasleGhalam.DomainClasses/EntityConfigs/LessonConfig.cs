using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LessonConfig : EntityTypeConfiguration<Lesson>
    {
        public LessonConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(200).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_Lesson_Name");

            this.HasRequired(x => x.GradeLevel)
               .WithMany(x => x.Lessons)
               .HasForeignKey(x => x.GradeLevelId)
               .WillCascadeOnDelete(false);

        }
    }
}
