namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLookupTbl2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.QuestionAnswers", "LookupId_AnswerType", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionAnswers", "Lookup_Id", c => c.Int());
            AddColumn("dbo.Questions", "LookupId_QuestionType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "LookupId_QuestionHardnessType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "LookupId_ReapetnessType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Lookup_Id", c => c.Int());
            AddColumn("dbo.AxillaryBooks", "LookupId_BookType", c => c.Int(nullable: false));
            AddColumn("dbo.AxillaryBooks", "LookupId_PaperType", c => c.Int(nullable: false));
            AddColumn("dbo.AxillaryBooks", "Lookup_Id", c => c.Int());
            AddColumn("dbo.Topics", "LookupId_HardnessType", c => c.Int(nullable: false));
            AddColumn("dbo.Topics", "LookupId_AreaType", c => c.Int(nullable: false));
            AddColumn("dbo.Topics", "Lookup_Id", c => c.Int());
            CreateIndex("dbo.AxillaryBooks", "Lookup_Id");
            CreateIndex("dbo.QuestionAnswers", "Lookup_Id");
            CreateIndex("dbo.Questions", "Lookup_Id");
            CreateIndex("dbo.Topics", "Lookup_Id");
            AddForeignKey("dbo.AxillaryBooks", "Lookup_Id", "dbo.Lookups", "Id");
            AddForeignKey("dbo.QuestionAnswers", "Lookup_Id", "dbo.Lookups", "Id");
            AddForeignKey("dbo.Questions", "Lookup_Id", "dbo.Lookups", "Id");
            AddForeignKey("dbo.Topics", "Lookup_Id", "dbo.Lookups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "Lookup_Id", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "Lookup_Id", "dbo.Lookups");
            DropForeignKey("dbo.QuestionAnswers", "Lookup_Id", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "Lookup_Id", "dbo.Lookups");
            DropIndex("dbo.Topics", new[] { "Lookup_Id" });
            DropIndex("dbo.Questions", new[] { "Lookup_Id" });
            DropIndex("dbo.QuestionAnswers", new[] { "Lookup_Id" });
            DropIndex("dbo.AxillaryBooks", new[] { "Lookup_Id" });
            DropColumn("dbo.Topics", "Lookup_Id");
            DropColumn("dbo.Topics", "LookupId_AreaType");
            DropColumn("dbo.Topics", "LookupId_HardnessType");
            DropColumn("dbo.AxillaryBooks", "Lookup_Id");
            DropColumn("dbo.AxillaryBooks", "LookupId_PaperType");
            DropColumn("dbo.AxillaryBooks", "LookupId_BookType");
            DropColumn("dbo.Questions", "Lookup_Id");
            DropColumn("dbo.Questions", "LookupId_ReapetnessType");
            DropColumn("dbo.Questions", "LookupId_QuestionHardnessType");
            DropColumn("dbo.Questions", "LookupId_QuestionType");
            DropColumn("dbo.QuestionAnswers", "Lookup_Id");
            DropColumn("dbo.QuestionAnswers", "LookupId_AnswerType");
            DropTable("dbo.Lookups");
        }
    }
}
