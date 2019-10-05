namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionrank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "LookupId_QuestionRank", c => c.Int(nullable: false , defaultValue:1063));
            CreateIndex("dbo.Questions", "LookupId_QuestionRank");
            AddForeignKey("dbo.Questions", "LookupId_QuestionRank", "dbo.Lookups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "LookupId_QuestionRank", "dbo.Lookups");
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionRank" });
            DropColumn("dbo.Questions", "LookupId_QuestionRank");
        }
    }
}
