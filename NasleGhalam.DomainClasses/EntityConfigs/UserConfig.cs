using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.Property(x => x.Family).HasMaxLength(50).IsRequired();
            this.Property(x => x.Username).HasMaxLength(50).IsRequired();
            this.HasIndex(x => x.Username).IsUnique().HasName("UK_User_Username");
            this.Property(x => x.Password).HasMaxLength(50).IsRequired();
            this.Property(x => x.NationalNo).HasMaxLength(10);
            this.HasIndex(x => x.NationalNo).IsUnique().HasName("UK_User_NationalNo");
            this.Property(x => x.Phone).HasMaxLength(8);
            this.Property(x => x.Mobile).HasMaxLength(10);


            this.HasRequired(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.City)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Student)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(false);
        }
    }
}
