namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fixLessonUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lessons_Users", name: "LessonId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Lessons_Users", name: "UserId", newName: "LessonId");
            RenameColumn(table: "dbo.Lessons_Users", name: "__mig_tmp__0", newName: "UserId");
            RenameIndex(table: "dbo.Lessons_Users", name: "IX_LessonId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Lessons_Users", name: "IX_UserId", newName: "IX_LessonId");
            RenameIndex(table: "dbo.Lessons_Users", name: "__mig_tmp__0", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lessons_Users", name: "IX_UserId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Lessons_Users", name: "IX_LessonId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Lessons_Users", name: "__mig_tmp__0", newName: "IX_LessonId");
            RenameColumn(table: "dbo.Lessons_Users", name: "UserId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Lessons_Users", name: "LessonId", newName: "UserId");
            RenameColumn(table: "dbo.Lessons_Users", name: "__mig_tmp__0", newName: "LessonId");
        }
    }
}
