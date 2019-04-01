namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class questionjudgeFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionJudges", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionJudges", "UserId");
            AddForeignKey("dbo.QuestionJudges", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionJudges", "UserId", "dbo.Users");
            DropIndex("dbo.QuestionJudges", new[] { "UserId" });
            DropColumn("dbo.QuestionJudges", "UserId");
        }
    }
}
