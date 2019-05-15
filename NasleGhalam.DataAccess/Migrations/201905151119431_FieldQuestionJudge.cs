namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldQuestionJudge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionJudges", "IsActiveQuestion", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionJudges", "IsActiveQuestionAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionJudges", "IsActiveQuestionAnswer");
            DropColumn("dbo.QuestionJudges", "IsActiveQuestion");
        }
    }
}
