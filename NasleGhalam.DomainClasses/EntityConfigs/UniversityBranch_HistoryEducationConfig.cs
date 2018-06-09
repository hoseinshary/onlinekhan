using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class UniversityBranch_HistoryEducationConfig : EntityTypeConfiguration<UniversityBranch_HistoryEducation>
    {
        public UniversityBranch_HistoryEducationConfig()
        {
            this.ToTable("UniversityBranches_HistoryEducations");
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.UniversityBranch)
                .WithMany(x => x.UniversityBranches_HistoryEducations)
                .HasForeignKey(x => x.UniversityBranchId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.HistoryEducation)
                .WithMany(x => x.UniversityBranches_HistoryEducations)
                .HasForeignKey(x => x.HistoryEducationId)
                .WillCascadeOnDelete(false);
        }
    }
}
