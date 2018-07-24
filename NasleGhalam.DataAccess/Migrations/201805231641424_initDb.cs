namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        ActionBit = c.Short(nullable: false),
                        Priority = c.Byte(nullable: false),
                        IsIndex = c.Boolean(nullable: false),
                        ControllerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controllers", t => t.ControllerId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.Controllers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                        Icone = c.String(nullable: false, maxLength: 200),
                        Priority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Level = c.Byte(nullable: false),
                        SumOfActionBit = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        IsAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.Username, unique: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Actions", "ControllerId", "dbo.Controllers");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Roles", new[] { "Name" });
            DropIndex("dbo.Actions", new[] { "ControllerId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Controllers");
            DropTable("dbo.Actions");
        }
    }
}
