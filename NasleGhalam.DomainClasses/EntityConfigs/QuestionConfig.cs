using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        public QuestionConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.AuthorName).HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(300);
            Property(x => x.FileName).IsRequired();

            HasRequired(x => x.User)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

         

            HasRequired(x => x.Lookup_QuestionType)
                .WithMany(x => x.Question_QuestionTypes)
                .HasForeignKey(x => x.LookupId_QuestionType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_QuestionHardnessType)
                .WithMany(x => x.Question_QuestionHardnessTypes)
                .HasForeignKey(x => x.LookupId_QuestionHardnessType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_RepeatnessType)
                .WithMany(x => x.Question_ReapetnessTypes)
                .HasForeignKey(x => x.LookupId_RepeatnessType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_AreaType)
                .WithMany(x => x.Question_AreaTypes)
                .HasForeignKey(x => x.LookupId_AreaType)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Lookup_AuthorType)
                .WithMany(x => x.Question_AuthorTypes)
                .HasForeignKey(x => x.LookupId_AuthorType)
                .WillCascadeOnDelete(false);


            HasMany(x => x.Tags)
                .WithMany(x => x.Questions)
                .Map(config =>
                {
                    config.MapLeftKey("QuestionId");
                    config.MapRightKey("TagId");
                    config.ToTable("Questions_Tags");
                });




            HasMany(x => x.Boxes)
                .WithMany(x => x.Questions)
                .Map(config =>
                {
                    config.MapLeftKey("QuestionId");
                    config.MapRightKey("BoxId");
                    config.ToTable("Questions_Boxes");
                });
        }
    }
}
