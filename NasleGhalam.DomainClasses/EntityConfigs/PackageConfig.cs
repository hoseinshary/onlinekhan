using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class PackageConfig : EntityTypeConfiguration<Package>
    {

        public PackageConfig()
        {
            HasKey(x => x.Id);



            HasMany(x => x.Assays)
                .WithMany(x => x.Packages)
                .Map(config =>
                {
                    config.MapLeftKey("PackageId");
                    config.MapRightKey("AssayId");
                    config.ToTable("Packages_Assays");
                });


            HasMany(x => x.Lessons)
                .WithMany(x => x.Packages)
                .Map(config =>
                {
                    config.MapLeftKey("PackageId");
                    config.MapRightKey("LessonId");
                    config.ToTable("Packages_Lessons");
                });

        }
    }
}
