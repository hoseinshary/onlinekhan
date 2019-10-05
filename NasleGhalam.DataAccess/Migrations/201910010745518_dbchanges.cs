namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionAnswers", "LessonName", c => c.String(maxLength: 50));
            AddColumn("dbo.Questions", "TopicAnswer", c => c.String(maxLength: 50));
            AddColumn("dbo.QuestionJudges", "LookupId_QuestionRank", c => c.Int(nullable: false , defaultValue : 1062));
            AddColumn("dbo.QuestionJudges", "EducationGroup", c => c.String(maxLength: 50));
            CreateIndex("dbo.QuestionJudges", "LookupId_QuestionRank");
            AddForeignKey("dbo.QuestionJudges", "LookupId_QuestionRank", "dbo.Lookups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionJudges", "LookupId_QuestionRank", "dbo.Lookups");
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_QuestionRank" });
            DropColumn("dbo.QuestionJudges", "EducationGroup");
            DropColumn("dbo.QuestionJudges", "LookupId_QuestionRank");
            DropColumn("dbo.Questions", "TopicAnswer");
            DropColumn("dbo.QuestionAnswers", "LessonName");
        }
    }
}
