namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLookupRelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AxillaryBooks", new[] { "Lookup_Id" });
            DropIndex("dbo.QuestionAnswers", new[] { "Lookup_Id" });
            DropIndex("dbo.Questions", new[] { "Lookup_Id" });
            DropIndex("dbo.Topics", new[] { "Lookup_Id" });
            DropColumn("dbo.AxillaryBooks", "LookupId_BookType");
            DropColumn("dbo.QuestionAnswers", "LookupId_AnswerType");
            DropColumn("dbo.Questions", "LookupId_QuestionHardnessType");
            DropColumn("dbo.Topics", "LookupId_AreaType");
            RenameColumn(table: "dbo.QuestionAnswers", name: "Lookup_Id", newName: "LookupId_AnswerType");
            RenameColumn(table: "dbo.AxillaryBooks", name: "Lookup_Id", newName: "LookupId_BookType");
            RenameColumn(table: "dbo.Questions", name: "Lookup_Id", newName: "LookupId_QuestionHardnessType");
            RenameColumn(table: "dbo.Topics", name: "Lookup_Id", newName: "LookupId_AreaType");
            AlterColumn("dbo.AxillaryBooks", "LookupId_BookType", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionAnswers", "LookupId_AnswerType", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "LookupId_QuestionHardnessType", c => c.Int(nullable: false));
            AlterColumn("dbo.Topics", "LookupId_AreaType", c => c.Int(nullable: false));
            CreateIndex("dbo.AxillaryBooks", "LookupId_PrintType");
            CreateIndex("dbo.AxillaryBooks", "LookupId_BookType");
            CreateIndex("dbo.AxillaryBooks", "LookupId_PaperType");
            CreateIndex("dbo.QuestionAnswers", "LookupId_AnswerType");
            CreateIndex("dbo.Questions", "LookupId_QuestionType");
            CreateIndex("dbo.Questions", "LookupId_QuestionHardnessType");
            CreateIndex("dbo.Questions", "LookupId_ReapetnessType");
            CreateIndex("dbo.Topics", "LookupId_HardnessType");
            CreateIndex("dbo.Topics", "LookupId_AreaType");
            AddForeignKey("dbo.Questions", "LookupId_QuestionType", "dbo.Lookups", "Id");
            AddForeignKey("dbo.Questions", "LookupId_ReapetnessType", "dbo.Lookups", "Id");
            AddForeignKey("dbo.Topics", "LookupId_HardnessType", "dbo.Lookups", "Id");
            AddForeignKey("dbo.AxillaryBooks", "LookupId_PaperType", "dbo.Lookups", "Id");
            AddForeignKey("dbo.AxillaryBooks", "LookupId_PrintType", "dbo.Lookups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PrintType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PaperType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LookupId_HardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_ReapetnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionType", "dbo.Lookups");
            DropIndex("dbo.Topics", new[] { "LookupId_AreaType" });
            DropIndex("dbo.Topics", new[] { "LookupId_HardnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_ReapetnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionHardnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.QuestionAnswers", new[] { "LookupId_AnswerType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PaperType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_BookType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PrintType" });
            AlterColumn("dbo.Topics", "LookupId_AreaType", c => c.Int());
            AlterColumn("dbo.Questions", "LookupId_QuestionHardnessType", c => c.Int());
            AlterColumn("dbo.QuestionAnswers", "LookupId_AnswerType", c => c.Int());
            AlterColumn("dbo.AxillaryBooks", "LookupId_BookType", c => c.Int());
            RenameColumn(table: "dbo.Topics", name: "LookupId_AreaType", newName: "Lookup_Id");
            RenameColumn(table: "dbo.Questions", name: "LookupId_QuestionHardnessType", newName: "Lookup_Id");
            RenameColumn(table: "dbo.AxillaryBooks", name: "LookupId_BookType", newName: "Lookup_Id");
            RenameColumn(table: "dbo.QuestionAnswers", name: "LookupId_AnswerType", newName: "Lookup_Id");
            AddColumn("dbo.Topics", "LookupId_AreaType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "LookupId_QuestionHardnessType", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionAnswers", "LookupId_AnswerType", c => c.Int(nullable: false));
            AddColumn("dbo.AxillaryBooks", "LookupId_BookType", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "Lookup_Id");
            CreateIndex("dbo.Questions", "Lookup_Id");
            CreateIndex("dbo.QuestionAnswers", "Lookup_Id");
            CreateIndex("dbo.AxillaryBooks", "Lookup_Id");
        }
    }
}
