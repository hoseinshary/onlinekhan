namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResume : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resumes", "PartnerJob", c => c.String(maxLength: 50));
            AddColumn("dbo.Resumes", "PartnerDegree", c => c.Int(nullable: false));
            AddColumn("dbo.Resumes", "PartnerPhone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Resumes", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Resumes", "Family", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Resumes", "FatherName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Resumes", "WifeJob");
            DropColumn("dbo.Resumes", "WifeDegree");
            DropColumn("dbo.Resumes", "WifePhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resumes", "WifePhone", c => c.String(maxLength: 50));
            AddColumn("dbo.Resumes", "WifeDegree", c => c.Int(nullable: false));
            AddColumn("dbo.Resumes", "WifeJob", c => c.String(maxLength: 50));
            AlterColumn("dbo.Resumes", "FatherName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Resumes", "Family", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Resumes", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Resumes", "PartnerPhone");
            DropColumn("dbo.Resumes", "PartnerDegree");
            DropColumn("dbo.Resumes", "PartnerJob");
        }
    }
}
