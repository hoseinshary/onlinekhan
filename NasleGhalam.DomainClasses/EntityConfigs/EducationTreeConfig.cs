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
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50);

            HasRequired(x => x.Lookup_EducationTree)
                .WithMany(x => x.EducationTree_States)
                .HasForeignKey(x => x.LookupId_EducationTree)
                .WillCascadeOnDelete(false);


            HasOptional(x => x.ParentEducationTree)
                .WithMany(x => x.ChildrenEducationTree)
                .HasForeignKey(x => x.ParentEducationTreeId)
                .WillCascadeOnDelete(false);


            HasMany(x => x.Lessons)
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
