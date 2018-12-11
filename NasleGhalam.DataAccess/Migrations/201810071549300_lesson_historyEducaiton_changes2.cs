namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lesson_historyEducaiton_changes2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "GradeLevelId", c => c.Int(nullable: false, defaultValue:1));
            CreateIndex("dbo.Lessons", "GradeLevelId");
            AddForeignKey("dbo.Lessons", "GradeLevelId", "dbo.GradeLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "GradeLevelId", "dbo.GradeLevels");
            DropIndex("dbo.Lessons", new[] { "GradeLevelId" });
            DropColumn("dbo.Lessons", "GradeLevelId");
        }
    }
}
