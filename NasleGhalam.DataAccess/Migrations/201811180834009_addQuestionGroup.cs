namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addQuestionGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        InsertTime = c.DateTime(nullable: false),
                        WordFile = c.String(maxLength: 50),
                        ExcelFile = c.String(maxLength: 50),
                        LessonId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionGroups_Questions",
                c => new
                    {
                        QuestionGroupId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionGroupId, t.QuestionId })
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionGroupId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionGroupId", "dbo.QuestionGroups");
            DropForeignKey("dbo.QuestionGroups", "LessonId", "dbo.Lessons");
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionGroupId" });
            DropIndex("dbo.QuestionGroups", new[] { "UserId" });
            DropIndex("dbo.QuestionGroups", new[] { "LessonId" });
            DropTable("dbo.QuestionGroups_Questions");
            DropTable("dbo.QuestionGroups");
        }
    }
}
