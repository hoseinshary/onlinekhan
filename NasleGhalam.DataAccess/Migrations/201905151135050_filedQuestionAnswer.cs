namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class filedQuestionAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionAnswers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionAnswers", "IsActive");
        }
    }
}
