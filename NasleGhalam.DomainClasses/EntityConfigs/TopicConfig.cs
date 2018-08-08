using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class TopicConfig : EntityTypeConfiguration<Topic>
    {
        public TopicConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Title).HasMaxLength(200).IsRequired();
            this.Property(x => x.ParentTopicId).IsOptional();

            this.HasRequired(x => x.EducationGroup_Lesson)
            .WithMany(x => x.Topics)
            .HasForeignKey(x => x.EducationGroup_LessonId)
            .WillCascadeOnDelete(false);

            this.HasRequired(x => x.ParentTopic)
                .WithMany(x => x.ParentTopics)
                .HasForeignKey(x => x.ParentTopicId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.HardnessType)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.HardnessTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.AreaType)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.AreaTypeId)
                .WillCascadeOnDelete(false);

            this.HasMany(x => x.EducationBooks)
                .WithMany(x => x.Topics)
                .Map(config =>
                {
                    config.MapLeftKey("TopicId");
                    config.MapRightKey("EducationBookId");
                    config.ToTable("Topics_EducationBooks");
                });


            this.HasMany(x => x.Questions)
                .WithMany(x => x.Topics)
                .Map(config =>
                {
                    config.MapLeftKey("TopicId");
                    config.MapRightKey("QuestionId");
                    config.ToTable("Topics_Questions");
                });

        }
    }
}
