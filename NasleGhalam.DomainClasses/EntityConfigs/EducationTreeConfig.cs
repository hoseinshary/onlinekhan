using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class EducationTreeConfig : EntityTypeConfiguration<EducationTree>
    {
        public EducationTreeConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasMaxLength(50);

            this.HasRequired(x => x.Lookup_EducationTree)
                .WithMany(x => x.EducationTree_States)
                .HasForeignKey(x => x.LookupId_EducationTree)
                .WillCascadeOnDelete(false);


            this.HasOptional(x => x.ParentEducationTree)
                .WithMany(x => x.ChildrenEducationTree)
                .HasForeignKey(x => x.ParentEducationTreeId)
                .WillCascadeOnDelete(false);


            this.HasMany(x => x.Lessons)
             .WithMany(x => x.EducationTrees)
             .Map(config =>
             {
                 config.MapLeftKey("EducationTreeId");
                 config.MapRightKey("LessonId");
                 config.ToTable("EducationTrees_Lessons");
             });


        }
    }
}
