namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMobileNumberLen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resumes", "Phone", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Resumes", "Mobile", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Resumes", "FatherPhone", c => c.String(maxLength: 11));
            AlterColumn("dbo.Resumes", "MotherPhone", c => c.String(maxLength: 11));
            AlterColumn("dbo.Resumes", "PartnerPhone", c => c.String(maxLength: 11));
            AlterColumn("dbo.Resumes", "PhoneReagent1", c => c.String(maxLength: 11));
            AlterColumn("dbo.Resumes", "PhoneReagent2", c => c.String(maxLength: 11));
            AlterColumn("dbo.Users", "Mobile", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Mobile", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "PhoneReagent2", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "PhoneReagent1", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "PartnerPhone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "MotherPhone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "FatherPhone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Resumes", "Mobile", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Resumes", "Phone", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
