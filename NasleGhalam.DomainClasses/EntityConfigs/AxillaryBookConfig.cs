using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AxillaryBookConfig : EntityTypeConfiguration<AxillaryBook>
    {
        public AxillaryBookConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.Property(x => x.Author).HasMaxLength(100).IsRequired();
            this.Property(x => x.Isbn).HasMaxLength(100).IsRequired();
            this.Property(x => x.Description).HasMaxLength(300).IsRequired();

            this.HasRequired(x => x.Publisher)
                .WithMany(x => x.AxillaryBooks)
                .HasForeignKey(x => x.PublisherId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.PrintType)
                .WithMany(x => x.AxillaryBooks)
                .HasForeignKey(x => x.PrintTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.BookType)
                .WithMany(x => x.AxillaryBooks)
                .HasForeignKey(x => x.BookTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.PaperType)
                .WithMany(x => x.AxillaryBooks)
                .HasForeignKey(x => x.PaperTypeId)
                .WillCascadeOnDelete(false);


            this.HasMany(x => x.EducationBooks)
                .WithMany(x => x.AxillaryBooks)
                .Map(config =>
                {
                    config.MapLeftKey("AxillaryBookId");
                    config.MapRightKey("EducationBookId");
                    config.ToTable("AxillaryBooks_EducationBooks");
                });
        }
    }
}
