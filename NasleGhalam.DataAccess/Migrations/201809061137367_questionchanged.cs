namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionchanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "WordFilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "WordFilePath", c => c.String(maxLength: 50));
        }
    }
}
