namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedAxillaryAndQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AxillaryBooks", "Description", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AxillaryBooks", "Description", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.Questions", "IsActive");
        }
    }
}
