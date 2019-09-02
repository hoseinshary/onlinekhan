namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldToQuestionJudge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionJudges", "LookupId_WhereProblem", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionJudges", "LookupId_ReasonProblem", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionJudges", "Description", c => c.String(maxLength: 50));
            CreateIndex("dbo.QuestionJudges", "LookupId_WhereProblem");
            CreateIndex("dbo.QuestionJudges", "LookupId_ReasonProblem");
            AddForeignKey("dbo.QuestionJudges", "LookupId_ReasonProblem", "dbo.Lookups", "Id");
            AddForeignKey("dbo.QuestionJudges", "LookupId_WhereProblem", "dbo.Lookups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionJudges", "LookupId_WhereProblem", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "LookupId_ReasonProblem", "dbo.Lookups");
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_ReasonProblem" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_WhereProblem" });
            DropColumn("dbo.QuestionJudges", "Description");
            DropColumn("dbo.QuestionJudges", "LookupId_ReasonProblem");
            DropColumn("dbo.QuestionJudges", "LookupId_WhereProblem");
        }
    }
}
