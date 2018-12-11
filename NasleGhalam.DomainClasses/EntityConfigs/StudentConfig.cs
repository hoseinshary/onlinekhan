using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.FatherName).HasMaxLength(50).IsRequired();
            this.Property(x => x.Address).HasMaxLength(300).IsRequired();
            //this.HasIndex(x => x.UserId).IsUnique();

            //this.HasRequired(x => x.User)
            //    .WithMany(x => x.Students)
            //    .HasForeignKey(x => x.UserId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
