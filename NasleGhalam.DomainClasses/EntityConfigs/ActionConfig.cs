using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ActionConfig : EntityTypeConfiguration<Action>
    {
        public ActionConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.FaName).HasMaxLength(50).IsRequired();

            this.HasRequired(x => x.Controller)
                .WithMany(x => x.Actions)
                .HasForeignKey(x => x.ControllerId)
                .WillCascadeOnDelete(false);
        }
    }
}
