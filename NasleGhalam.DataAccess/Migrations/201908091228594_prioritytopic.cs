namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prioritytopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "DisplayPriority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "DisplayPriority");
        }
    }
}
