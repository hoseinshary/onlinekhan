using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        public QuestionConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Context).IsRequired().HasColumnType("nvarchar(max)");
            this.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            this.Property(x => x.Description).HasMaxLength(300);

            this.HasRequired(x => x.User)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.AxillaryBook)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.AxillaryBookId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.QuestionType)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuestionTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.QuestionHardnessType)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuestionHardnessTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.ReapetnessType)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.ReapetnessTypeId)
                .WillCascadeOnDelete(false);

            this.HasMany(x => x.Tags)
                .WithMany(x => x.Questions)
                .Map(config =>
                {
                    config.MapLeftKey("QuestionId");
                    config.MapRightKey("TagId");
                    config.ToTable("Questions_Tags");
                });

            this.HasMany(x => x.Boxes)
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
