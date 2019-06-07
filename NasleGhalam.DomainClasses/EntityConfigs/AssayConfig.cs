using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssayConfig : EntityTypeConfiguration<Assay>
    {
        public AssayConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(50).IsRequired();

            HasRequired(x=>x.User)
                .WithMany(x=>x.Assays)
                .HasForeignKey(x=>x.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_QuestionType)
                .WithMany(x => x.Assay_QuestionType)
                .HasForeignKey(x => x.LookupId_QuestionType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_Importance)
                .WithMany(x => x.Assay_Importance)
                .HasForeignKey(x => x.LookupId_Importance)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_Type)
                .WithMany(x => x.Assay_Type)
                .HasForeignKey(x => x.LookupId_Type)
                .WillCascadeOnDelete(false);
        }
    }
}
