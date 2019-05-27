namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lessonuserAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons_Users",
                c => new
                    {
                        LessonId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LessonId, t.UserId })
                .ForeignKey("dbo.Users", t => t.LessonId)
                .ForeignKey("dbo.Lessons", t => t.UserId)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons_Users", "UserId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons_Users", "LessonId", "dbo.Users");
            DropIndex("dbo.Lessons_Users", new[] { "UserId" });
            DropIndex("dbo.Lessons_Users", new[] { "LessonId" });
            DropTable("dbo.Lessons_Users");
        }
    }
}
