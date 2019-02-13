namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedQuestionGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionGroups", "File", c => c.String(maxLength: 50));
            DropColumn("dbo.QuestionGroups", "WordFile");
            DropColumn("dbo.QuestionGroups", "ExcelFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionGroups", "ExcelFile", c => c.String(maxLength: 50));
            AddColumn("dbo.QuestionGroups", "WordFile", c => c.String(maxLength: 50));
            DropColumn("dbo.QuestionGroups", "File");
        }
    }
}
