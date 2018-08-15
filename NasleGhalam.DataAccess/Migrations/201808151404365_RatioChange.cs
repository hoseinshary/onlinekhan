namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatioChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratios", "Name", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratios", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
