using System;
using System.Data.Entity.ModelConfiguration;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.DomainClasses.EntityConfigs
{
    public class ResumeConfig : EntityTypeConfiguration<Resume>
    {
        public ResumeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Family).HasMaxLength(50).IsRequired();
            Property(x => x.FatherName).HasMaxLength(50).IsRequired();
            Property(x => x.NationalNo).HasMaxLength(10);
            Property(x => x.Phone).HasMaxLength(8);
            Property(x => x.Mobile).HasMaxLength(10);
            Property(x => x.Gender).IsRequired();
            Property(x => x.CityBorn).HasMaxLength(50).IsRequired();
            Property(x => x.Birthday).IsRequired();
            Property(x => x.Marriage).IsRequired();
            Property(x => x.Address).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.Description).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.EducationCertificateJSON).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.PublicationJSON).IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.TeachingResumeJSON).IsRequired().HasColumnType("nvarchar(max)");





            HasRequired(x => x.City)
                .WithMany(x => x.Resumes)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

        }
    }
}