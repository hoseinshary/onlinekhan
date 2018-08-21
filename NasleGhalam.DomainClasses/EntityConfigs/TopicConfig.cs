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
            this.HasIndex(x => x.Title).IsUnique().HasName("UK_Topic_Name");
            this.Property(x => x.ParentTopicId).IsOptional();

            this.HasRequired(x => x.EducationGroup_Lesson)
            .WithMany(x => x.Topics)
            .HasForeignKey(x => x.EducationGroup_LessonId)
            .WillCascadeOnDelete(false);

            this.HasOptional(x => x.ParentTopic)
                .WithMany(x => x.ParentTopics)
                .HasForeignKey(x => x.ParentTopicId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_HardnessType)
                .WithMany(x => x.Topic_Hardnesses)
                .HasForeignKey(x => x.LookupId_HardnessType)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_AreaType)
                .WithMany(x => x.Topic_AreaTypes)
                .HasForeignKey(x => x.LookupId_AreaType)
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
