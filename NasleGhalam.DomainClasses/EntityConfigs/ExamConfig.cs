using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ExamConfig : EntityTypeConfiguration<Exam>
    {
        public ExamConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();

            this.HasRequired(x => x.EducationGroup)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.EducationGroupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.EducationYear)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.EducationYearId)
                .WillCascadeOnDelete(false);
        }
    }
}
