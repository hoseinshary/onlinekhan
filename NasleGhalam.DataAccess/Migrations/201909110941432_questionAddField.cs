namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionAddField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsHybrid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsHybrid");
        }
    }
}
