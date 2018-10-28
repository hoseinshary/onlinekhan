using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionJudgConfig : EntityTypeConfiguration<QuestionJudge>
    {
        public QuestionJudgConfig()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Question)
                .WithMany(x => x.QuestionJudges)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);
        }
    }
}
