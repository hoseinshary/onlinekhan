using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class AssaySchaduleConfig : EntityTypeConfiguration<AssaySchadule>
    {
        public AssaySchaduleConfig()
        {
            HasKey(x => x.Id);

            

            HasRequired(x => x.Assay)
                .WithMany(x => x.AssaySchadules)
                .HasForeignKey(x => x.AssayId)
                .WillCascadeOnDelete(false);

            

        }
    }
}
