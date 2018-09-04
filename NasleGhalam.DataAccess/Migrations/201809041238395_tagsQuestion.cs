namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsQuestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions_AxillaryBooks", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions_AxillaryBooks", "AxillaryBookId", "dbo.AxillaryBooks");
            DropIndex("dbo.Questions_AxillaryBooks", new[] { "QuestionId" });
            DropIndex("dbo.Questions_AxillaryBooks", new[] { "AxillaryBookId" });
            RenameColumn(table: "dbo.Questions", name: "LookupId_ReapetnessType", newName: "LookupId_RepeatnessType");
            RenameIndex(table: "dbo.Questions", name: "IX_LookupId_ReapetnessType", newName: "IX_LookupId_RepeatnessType");
            AddColumn("dbo.Questions", "LookupId_AuthorType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "LookupId_AreaType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Lookup_AreaType_Id", c => c.Int());
            AddColumn("dbo.Questions", "Lookup_AuthorType_Id", c => c.Int());
            AddColumn("dbo.Tags", "isSource", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Questions", "Lookup_AreaType_Id");
            CreateIndex("dbo.Questions", "Lookup_AuthorType_Id");
            AddForeignKey("dbo.Questions", "Lookup_AreaType_Id", "dbo.Lookups", "Id");
            AddForeignKey("dbo.Questions", "Lookup_AuthorType_Id", "dbo.Lookups", "Id");
            DropColumn("dbo.Questions", "AuthorType");
            DropTable("dbo.Questions_AxillaryBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Questions_AxillaryBooks",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        AxillaryBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.AxillaryBookId });
            
            AddColumn("dbo.Questions", "AuthorType", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Questions", "Lookup_AuthorType_Id", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "Lookup_AreaType_Id", "dbo.Lookups");
            DropIndex("dbo.Questions", new[] { "Lookup_AuthorType_Id" });
            DropIndex("dbo.Questions", new[] { "Lookup_AreaType_Id" });
            DropColumn("dbo.Tags", "isSource");
            DropColumn("dbo.Questions", "Lookup_AuthorType_Id");
            DropColumn("dbo.Questions", "Lookup_AreaType_Id");
            DropColumn("dbo.Questions", "LookupId_AreaType");
            DropColumn("dbo.Questions", "LookupId_AuthorType");
            RenameIndex(table: "dbo.Questions", name: "IX_LookupId_RepeatnessType", newName: "IX_LookupId_ReapetnessType");
            RenameColumn(table: "dbo.Questions", name: "LookupId_RepeatnessType", newName: "LookupId_ReapetnessType");
            CreateIndex("dbo.Questions_AxillaryBooks", "AxillaryBookId");
            CreateIndex("dbo.Questions_AxillaryBooks", "QuestionId");
            AddForeignKey("dbo.Questions_AxillaryBooks", "AxillaryBookId", "dbo.AxillaryBooks", "Id");
            AddForeignKey("dbo.Questions_AxillaryBooks", "QuestionId", "dbo.Questions", "Id");
        }
    }
}
