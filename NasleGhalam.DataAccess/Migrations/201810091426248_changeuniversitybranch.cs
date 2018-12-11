namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class changeuniversitybranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UniversityBranches", "SiteAverage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UniversityBranches", "SiteAverage");
        }
    }
}
