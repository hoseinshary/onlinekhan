namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionratiochanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "FileName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Ratios", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratios", "Name", c => c.String(maxLength: 200));
            DropColumn("dbo.Questions", "FileName");
        }
    }
}
