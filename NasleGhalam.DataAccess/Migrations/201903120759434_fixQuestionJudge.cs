namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixQuestionJudge : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.QuestionJudges", "Repeatness");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionJudges", "Repeatness", c => c.Byte(nullable: false));
        }
    }
}
