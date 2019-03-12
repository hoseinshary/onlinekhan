namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixQuestionjudgeRel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.QuestionJudges", new[] { "Lookup_QuestionHardnessType_Id" });
            DropIndex("dbo.QuestionJudges", new[] { "Lookup_RepeatnessType_Id" });
            DropColumn("dbo.QuestionJudges", "LookupId_QuestionHardnessType");
            DropColumn("dbo.QuestionJudges", "LookupId_RepeatnessType");
            RenameColumn(table: "dbo.QuestionJudges", name: "Lookup_QuestionHardnessType_Id", newName: "LookupId_QuestionHardnessType");
            RenameColumn(table: "dbo.QuestionJudges", name: "Lookup_RepeatnessType_Id", newName: "LookupId_RepeatnessType");
            AlterColumn("dbo.QuestionJudges", "LookupId_QuestionHardnessType", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionJudges", "LookupId_RepeatnessType", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionJudges", "LookupId_QuestionHardnessType");
            CreateIndex("dbo.QuestionJudges", "LookupId_RepeatnessType");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_RepeatnessType" });
            DropIndex("dbo.QuestionJudges", new[] { "LookupId_QuestionHardnessType" });
            AlterColumn("dbo.QuestionJudges", "LookupId_RepeatnessType", c => c.Int());
            AlterColumn("dbo.QuestionJudges", "LookupId_QuestionHardnessType", c => c.Int());
            RenameColumn(table: "dbo.QuestionJudges", name: "LookupId_RepeatnessType", newName: "Lookup_RepeatnessType_Id");
            RenameColumn(table: "dbo.QuestionJudges", name: "LookupId_QuestionHardnessType", newName: "Lookup_QuestionHardnessType_Id");
            AddColumn("dbo.QuestionJudges", "LookupId_RepeatnessType", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionJudges", "LookupId_QuestionHardnessType", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionJudges", "Lookup_RepeatnessType_Id");
            CreateIndex("dbo.QuestionJudges", "Lookup_QuestionHardnessType_Id");
        }
    }
}
