namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class OneToOneStudentAndUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.Students", "Id");
            AddForeignKey("dbo.Students", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students", "Id");
            AddForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropIndex("dbo.Students", new[] { "Id" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "Id");
            AddForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students", "Id");
            AddForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students", "Id");
        }
    }
}
