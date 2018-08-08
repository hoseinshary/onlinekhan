namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLookupTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AxillaryBooks", "LookupId_PrintType", c => c.Int(nullable: false));
            DropColumn("dbo.QuestionAnswers", "AnswerType");
            DropColumn("dbo.Questions", "QuestionType");
            DropColumn("dbo.Questions", "QuestionHardnessType");
            DropColumn("dbo.Questions", "ReapetnessType");
            DropColumn("dbo.AxillaryBooks", "PrintType");
            DropColumn("dbo.AxillaryBooks", "PaperType");
            DropColumn("dbo.Topics", "HardnessType");
            DropColumn("dbo.Topics", "AreaType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "AreaType", c => c.Byte(nullable: false));
            AddColumn("dbo.Topics", "HardnessType", c => c.Byte(nullable: false));
            AddColumn("dbo.AxillaryBooks", "PaperType", c => c.Byte(nullable: false));
            AddColumn("dbo.AxillaryBooks", "PrintType", c => c.Byte(nullable: false));
            AddColumn("dbo.Questions", "ReapetnessType", c => c.Byte(nullable: false));
            AddColumn("dbo.Questions", "QuestionHardnessType", c => c.Byte(nullable: false));
            AddColumn("dbo.Questions", "QuestionType", c => c.Byte(nullable: false));
            AddColumn("dbo.QuestionAnswers", "AnswerType", c => c.Byte(nullable: false));
            DropColumn("dbo.AxillaryBooks", "LookupId_PrintType");
        }
    }
}
