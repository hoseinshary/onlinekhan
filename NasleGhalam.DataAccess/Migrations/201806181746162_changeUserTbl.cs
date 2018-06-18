namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserTbl : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Users", name: "IX_Username", newName: "UK_User_Username");
            AlterColumn("dbo.Provinces", "Code", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Provinces", "Code", c => c.String(maxLength: 50));
            RenameIndex(table: "dbo.Users", name: "UK_User_Username", newName: "IX_Username");
        }
    }
}
