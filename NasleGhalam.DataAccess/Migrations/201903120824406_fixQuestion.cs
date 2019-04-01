namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fixQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "IsUpdate", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsUpdate");
            DropColumn("dbo.Questions", "IsDelete");
        }
    }
}
