namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddResume : DbMigration
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
                        Name = c.String(nullable: false, maxLength: 150),
                        Family = c.String(nullable: false, maxLength: 150),
                        FatherName = c.String(nullable: false, maxLength: 150),
                        IdNumber = c.String(maxLength: 10),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 8),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        CityBorn = c.String(nullable: false, maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        Marriage = c.Boolean(nullable: false),
                        Religion = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                        PostCode = c.String(nullable: false, maxLength: 10),
                        FatherJob = c.String(maxLength: 150),
                        FatherDegree = c.Int(nullable: false),
                        FatherPhone = c.String(maxLength: 10),
                        MotherJob = c.String(maxLength: 150),
                        MotherDegree = c.Int(nullable: false),
                        MotherPhone = c.String(maxLength: 10),
                        PartnerJob = c.String(maxLength: 150),
                        PartnerDegree = c.Int(nullable: false),
                        PartnerPhone = c.String(maxLength: 10),
                        Reagent1 = c.String(maxLength: 50),
                        RelationReagent1 = c.String(maxLength: 50),
                        JobReagent1 = c.String(maxLength: 50),
                        PhoneReagent1 = c.String(maxLength: 10),
                        AddressReagent1 = c.String(maxLength: 300),
                        Reagent2 = c.String(maxLength: 50),
                        RelationReagent2 = c.String(maxLength: 50),
                        JobReagent2 = c.String(maxLength: 50),
                        PhoneReagent2 = c.String(maxLength: 10),
                        AddressReagent2 = c.String(maxLength: 300),
                        HaveEducationCertificate = c.Boolean(nullable: false),
                        HaveAnotherCertificate = c.Boolean(nullable: false),
                        AnotherCertificate = c.String(maxLength: 50),
                        HavePublication = c.Boolean(nullable: false),
                        NumberOfPublication = c.Int(nullable: false),
                        HaveTeachingResume = c.Boolean(nullable: false),
                        NumberOfTeachingYear = c.Int(nullable: false),
                        TeachingOrPublishingRequest1 = c.Boolean(nullable: false),
                        MaghtaRequest1 = c.Int(nullable: false),
                        KindRequest1 = c.Int(nullable: false),
                        LessonNameRequest1 = c.String(maxLength: 50),
                        TeachingOrPublishingRequest2 = c.Boolean(nullable: false),
                        MaghtaRequest2 = c.Int(nullable: false),
                        KindRequest2 = c.Int(nullable: false),
                        LessonNameRequest2 = c.String(maxLength: 50),
                        RequestForAdvice = c.Boolean(nullable: false),
                        MaghtaAdvice = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        EducationCertificateJson = c.String(nullable: false),
                        PublicationJson = c.String(nullable: false),
                        TeachingResumeJson = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
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
