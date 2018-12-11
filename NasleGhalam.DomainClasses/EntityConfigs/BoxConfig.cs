using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class BoxConfig : EntityTypeConfiguration<Box>
    {
        public BoxConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();

            this.HasRequired(x => x.Teacher)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.TeacherId)
                .WillCascadeOnDelete(false);
        }
    }
}
