using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionAnswerConfig : EntityTypeConfiguration<QuestionAnswer>
    {
        public QuestionAnswerConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.FilePath).HasMaxLength(200);
            this.Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");
            this.Property(x => x.Description).HasMaxLength(300);
            this.Property(x => x.Author).HasMaxLength(100);

            this.HasRequired(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.User)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_AnswerType)
                .WithMany(x => x.QuestionAnswers)
                .HasForeignKey(x => x.LookupId_AnswerType)
                .WillCascadeOnDelete(false);
        }
    }
}
