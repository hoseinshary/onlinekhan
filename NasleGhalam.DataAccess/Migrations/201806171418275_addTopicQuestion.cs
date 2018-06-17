namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTopicQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics_Questions",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.QuestionId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.TopicId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Topics_Questions", "TopicId", "dbo.Topics");
            DropIndex("dbo.Topics_Questions", new[] { "QuestionId" });
            DropIndex("dbo.Topics_Questions", new[] { "TopicId" });
            DropTable("dbo.Topics_Questions");
        }
    }
}
