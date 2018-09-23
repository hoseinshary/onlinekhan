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
            this.HasIndex(x => x.Name).IsUnique().HasName("UK_AxillaryBook_Name");
            this.Property(x => x.Author).HasMaxLength(100).IsRequired();
            this.Property(x => x.Isbn).HasMaxLength(100).IsRequired();
            this.Property(x => x.Description).HasMaxLength(300).IsRequired();
            this.Property(x => x.ImgName).HasMaxLength(200);

            this.HasRequired(x => x.Publisher)
                .WithMany(x => x.AxillaryBooks)
                .HasForeignKey(x => x.PublisherId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_PrintType)
                .WithMany(x => x.AxillaryBook_PrintTypes)
                .HasForeignKey(x => x.LookupId_PrintType)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_BookType)
                .WithMany(x => x.AxillaryBook_BookTypes)
                .HasForeignKey(x => x.LookupId_BookType)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Lookup_PaperType)
                .WithMany(x => x.AxillaryBook_PaperTypes)
                .HasForeignKey(x => x.LookupId_PaperType)
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
