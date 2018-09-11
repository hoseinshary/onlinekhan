namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oneToOneStudentAndUser : DbMigration
    {
        public override void Up()
        {
            // khodom ezaf kerdam
            //DropPrimaryKey("dbo.Students", "Id");
            //Sql("ALTER TABLE Students DROP CONSTRAINT DF_Students_Id");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropIndex("dbo.Students", new[] { "UserId" });
            DropColumn("dbo.Students", "Id");
            RenameColumn(table: "dbo.Students", name: "UserId", newName: "Id");
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.Students", "Id");
            AddForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students", "Id");
            AddForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            // khodom ezaf kerdam
            //DropPrimaryKey("dbo.Students", "Id");
            //Sql("ALTER TABLE Students DROP CONSTRAINT DF_Students_Id");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropIndex("dbo.Students", new[] { "Id" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "Id");
            RenameColumn(table: "dbo.Students", name: "Id", newName: "UserId");
            AddColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Students", "UserId", unique: true);
            AddForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students", "Id");
            AddForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students", "Id");
        }
    }
}
