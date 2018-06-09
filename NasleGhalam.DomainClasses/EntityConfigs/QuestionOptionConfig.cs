using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionOptionConfig : EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");

            this.HasRequired(x => x.Question)
                .WithMany(x => x.QuestionOptions)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);
        }
    }
}
