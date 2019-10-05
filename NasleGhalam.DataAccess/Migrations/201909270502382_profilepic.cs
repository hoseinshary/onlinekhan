namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profilepic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfilePic", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ProfilePic");
        }
    }
}
