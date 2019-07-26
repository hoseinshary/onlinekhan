namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resumeadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Branch = c.String(maxLength: 50),
                        CreationDateTime = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        IdNumber = c.String(maxLength: 50),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 8),
                        Mobile = c.String(maxLength: 10),
                        CityBorn = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        Marriage = c.Boolean(nullable: false),
                        Religion = c.String(maxLength: 50),
                        Address = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
                        PostCode = c.String(maxLength: 50),
                        FatherJob = c.String(maxLength: 50),
                        FatherDegree = c.Int(nullable: false),
                        FatherPhone = c.String(maxLength: 50),
                        MotherJob = c.String(maxLength: 50),
                        MotherDegree = c.Int(nullable: false),
                        MotherPhone = c.String(maxLength: 50),
                        WifeJob = c.String(maxLength: 50),
                        WifeDegree = c.Int(nullable: false),
                        WifePhone = c.String(maxLength: 50),
                        Reagent1 = c.String(maxLength: 50),
                        RelationReagent1 = c.String(maxLength: 50),
                        JobReagent1 = c.String(maxLength: 50),
                        PhoneReagent1 = c.String(maxLength: 50),
                        AddressReagent1 = c.String(maxLength: 50),
                        Reagent2 = c.String(maxLength: 50),
                        RelationReagent2 = c.String(maxLength: 50),
                        JobReagent2 = c.String(maxLength: 50),
                        PhoneReagent2 = c.String(maxLength: 50),
                        AddressReagent2 = c.String(maxLength: 50),
                        HaveEducationCertificate = c.Boolean(nullable: false),
                        HaveAnotherCertificate = c.Boolean(nullable: false),
                        AnotherCertificate = c.String(maxLength: 50),
                        EducationCertificateJSON = c.String(nullable: false),
                        HavePublication = c.Boolean(nullable: false),
                        NumberOfPublication = c.Int(nullable: false),
                        PublicationJSON = c.String(nullable: false),
                        HaveTeachingResume = c.Boolean(nullable: false),
                        NumberOfTeachingYear = c.Int(nullable: false),
                        TeachingResumeJSON = c.String(nullable: false),
                        TeachingOrPublishingRequest1 = c.Boolean(nullable: false),
                        MaghtaRequest1 = c.Int(nullable: false),
                        KindRequestRequest1 = c.Int(nullable: false),
                        LessonNameRequest1 = c.String(maxLength: 50),
                        TeachingOrPublishingRequest2 = c.Boolean(nullable: false),
                        MaghtaRequest2 = c.Int(nullable: false),
                        KindRequestRequest2 = c.Int(nullable: false),
                        LessonNameRequest2 = c.String(maxLength: 50),
                        RequestForAdvice = c.Boolean(nullable: false),
                        MaghtaAdvice = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resumes", "CityId", "dbo.Cities");
            DropIndex("dbo.Resumes", new[] { "CityId" });
            DropTable("dbo.Resumes");
        }
    }
}
