namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionWriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "WriterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "WriterId");
            AddForeignKey("dbo.Questions", "WriterId", "dbo.Writers", "Id");
            DropColumn("dbo.Questions", "AuthorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "AuthorName", c => c.String(maxLength: 100));
            DropForeignKey("dbo.Questions", "WriterId", "dbo.Writers");
            DropIndex("dbo.Questions", new[] { "WriterId" });
            DropColumn("dbo.Questions", "WriterId");
        }
    }
}
