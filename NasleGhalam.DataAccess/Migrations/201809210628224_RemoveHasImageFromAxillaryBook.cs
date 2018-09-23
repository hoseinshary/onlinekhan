namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHasImageFromAxillaryBook : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AxillaryBooks", "HasImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AxillaryBooks", "HasImage", c => c.Boolean(nullable: false));
        }
    }
}
