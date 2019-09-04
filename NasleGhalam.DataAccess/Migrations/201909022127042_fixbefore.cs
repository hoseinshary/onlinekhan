namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixbefore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionJudges", "Description", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionJudges", "Description", c => c.String(maxLength: 50));
        }
    }
}
