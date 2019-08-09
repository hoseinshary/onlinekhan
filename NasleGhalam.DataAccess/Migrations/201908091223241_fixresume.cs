namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixresume : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resumes", "TeachingRequest1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resumes", "PublishingRequest1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resumes", "TeachingRequest2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resumes", "PublishingRequest2", c => c.Boolean(nullable: false));
            DropColumn("dbo.Resumes", "TeachingOrPublishingRequest1");
            DropColumn("dbo.Resumes", "TeachingOrPublishingRequest2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resumes", "TeachingOrPublishingRequest2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Resumes", "TeachingOrPublishingRequest1", c => c.Boolean(nullable: false));
            DropColumn("dbo.Resumes", "PublishingRequest2");
            DropColumn("dbo.Resumes", "TeachingRequest2");
            DropColumn("dbo.Resumes", "PublishingRequest1");
            DropColumn("dbo.Resumes", "TeachingRequest1");
        }
    }
}
