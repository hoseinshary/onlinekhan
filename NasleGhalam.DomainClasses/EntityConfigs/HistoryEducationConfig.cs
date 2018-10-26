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

            

            
            this.HasRequired(x => x.EducationTree)
                .WithMany(x => x.HistoryEducations)
                .HasForeignKey(x => x.EducationTreeId)
                .WillCascadeOnDelete(false);

            this.HasMany(x => x.Cities)
             .WithMany(x => x.HistoryEducations)
             .Map(config =>
             {
                 config.MapLeftKey("HistoryEducationId");
                 config.MapRightKey("CityId");
                 config.ToTable("HistoryEducations_Cities");
             });

            this.HasMany(x => x.UniversityBranches)
             .WithMany(x => x.HistoryEducations)
             .Map(config =>
             {
                 config.MapLeftKey("HistoryEducationId");
                 config.MapRightKey("UniversityBranchId");
                 config.ToTable("HistoryEducations_UniversityBranchs");
             });



        }
    }
}
