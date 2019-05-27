namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lessonField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "NumberOfJudges", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "NumberOfJudges");
        }
    }
}
