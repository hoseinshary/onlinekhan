using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class LessonConfig : EntityTypeConfiguration<Lesson>
    {
        public LessonConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(200).IsRequired();
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_Lesson_Name");




            this.HasRequired(x => x.Lookup_Nezam)
              .WithMany(x => x.Lesson_Nezams)
              .HasForeignKey(x => x.LookupId_Nezam)
              .WillCascadeOnDelete(false);
        }
    }
}
