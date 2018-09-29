namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class questionisrequire : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "AuthorName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "AuthorName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
