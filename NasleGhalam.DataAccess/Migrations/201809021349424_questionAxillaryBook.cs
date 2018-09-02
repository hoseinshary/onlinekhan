namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionAxillaryBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "AxillaryBookId", "dbo.AxillaryBooks");
            DropIndex("dbo.Questions", new[] { "AxillaryBookId" });
            CreateTable(
                "dbo.Questions_AxillaryBooks",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        AxillaryBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.AxillaryBookId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.AxillaryBooks", t => t.AxillaryBookId)
                .Index(t => t.QuestionId)
                .Index(t => t.AxillaryBookId);
            
            AddColumn("dbo.Questions", "WordFilePath", c => c.String(maxLength: 50));
            DropColumn("dbo.Questions", "AxillaryBookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "AxillaryBookId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions_AxillaryBooks", "AxillaryBookId", "dbo.AxillaryBooks");
            DropForeignKey("dbo.Questions_AxillaryBooks", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions_AxillaryBooks", new[] { "AxillaryBookId" });
            DropIndex("dbo.Questions_AxillaryBooks", new[] { "QuestionId" });
            DropColumn("dbo.Questions", "WordFilePath");
            DropTable("dbo.Questions_AxillaryBooks");
            CreateIndex("dbo.Questions", "AxillaryBookId");
            AddForeignKey("dbo.Questions", "AxillaryBookId", "dbo.AxillaryBooks", "Id");
        }
    }
}
