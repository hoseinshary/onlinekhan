namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonNameUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Lessons", "Name", unique: true, name: "UK_Lesson_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lessons", "UK_Lesson_Name");
        }
    }
}
