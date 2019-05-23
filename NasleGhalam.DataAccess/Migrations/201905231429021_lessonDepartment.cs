namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lessonDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LessonDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_LessonDepartment_Name");
            
            CreateTable(
                "dbo.Lessons_LessonDepartments",
                c => new
                    {
                        LessonId = c.Int(nullable: false),
                        LessonDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LessonId, t.LessonDepartmentId })
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.LessonDepartments", t => t.LessonDepartmentId)
                .Index(t => t.LessonId)
                .Index(t => t.LessonDepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonDepartmentId", "dbo.LessonDepartments");
            DropForeignKey("dbo.Lessons_LessonDepartments", "LessonId", "dbo.Lessons");
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonDepartmentId" });
            DropIndex("dbo.Lessons_LessonDepartments", new[] { "LessonId" });
            DropIndex("dbo.LessonDepartments", "UK_LessonDepartment_Name");
            DropTable("dbo.Lessons_LessonDepartments");
            DropTable("dbo.LessonDepartments");
        }
    }
}
