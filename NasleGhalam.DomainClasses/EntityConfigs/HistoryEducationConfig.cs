using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class HistoryEducationConfig : EntityTypeConfiguration<HistoryEducation>
    {
        public HistoryEducationConfig()
        {
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Student)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.StudentId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Exam)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.ExamId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.EducationGroup)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.EducationGroupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.EducationSubGroup)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.EducationSubGroupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.GradeLevel)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.GradeLevelId)
                .WillCascadeOnDelete(false);
        }
    }
}
