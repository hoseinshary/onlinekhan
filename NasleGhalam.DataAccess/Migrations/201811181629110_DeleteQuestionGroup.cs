namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DeleteQuestionGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionGroups", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionGroupId", "dbo.QuestionGroups");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionGroups", "UserId", "dbo.Users");
            DropIndex("dbo.QuestionGroups", new[] { "LessonId" });
            DropIndex("dbo.QuestionGroups", new[] { "UserId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionGroupId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionId" });
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.QuestionGroups_Questions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionGroups_Questions",
                c => new
                    {
                        QuestionGroupId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionGroupId, t.QuestionId });
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.QuestionGroups_Questions", "QuestionId");
            CreateIndex("dbo.QuestionGroups_Questions", "QuestionGroupId");
            CreateIndex("dbo.QuestionGroups", "UserId");
            CreateIndex("dbo.QuestionGroups", "LessonId");
            AddForeignKey("dbo.QuestionGroups", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.QuestionGroups_Questions", "QuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.QuestionGroups_Questions", "QuestionGroupId", "dbo.QuestionGroups", "Id");
            AddForeignKey("dbo.QuestionGroups", "LessonId", "dbo.Lessons", "Id");
        }
    }
}
