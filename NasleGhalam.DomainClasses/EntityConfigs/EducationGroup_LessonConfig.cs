using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationGroup_LessonConfig : EntityTypeConfiguration<EducationGroup_Lesson>
    {
        public EducationGroup_LessonConfig()
        {
            this.ToTable("EducationGroups_Lessons");
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.EducationGroup)
                .WithMany(x => x.EducationGroups_Lessons)
                .HasForeignKey(x => x.EducationGroupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lesson)
                .WithMany(x => x.EducationGroups_Lessons)
                .HasForeignKey(x => x.LessonId)
                .WillCascadeOnDelete(false);
        }
    }
}
