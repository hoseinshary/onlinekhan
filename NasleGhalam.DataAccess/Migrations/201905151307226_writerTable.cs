namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class writerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.QuestionAnswers", "WriterId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionAnswers", "WriterId");
            AddForeignKey("dbo.QuestionAnswers", "WriterId", "dbo.Writers", "Id");
            DropColumn("dbo.QuestionAnswers", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionAnswers", "Author", c => c.String(maxLength: 100));
            DropForeignKey("dbo.QuestionAnswers", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Writers", "User_Id", "dbo.Users");
            DropIndex("dbo.Writers", new[] { "User_Id" });
            DropIndex("dbo.QuestionAnswers", new[] { "WriterId" });
            DropColumn("dbo.QuestionAnswers", "WriterId");
            DropTable("dbo.Writers");
        }
    }
}
