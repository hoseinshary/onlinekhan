using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class UniversityBranchConfig : EntityTypeConfiguration<UniversityBranch>
    {
        public UniversityBranchConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();

            this.HasRequired(x => x.EducationSubGroup)
                .WithMany(x => x.UniversityBranches)
                .HasForeignKey(x => x.EducationSubGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}
