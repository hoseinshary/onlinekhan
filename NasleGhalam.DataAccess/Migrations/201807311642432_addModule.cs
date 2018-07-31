namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addModule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Priority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Module_Name");
            
            AddColumn("dbo.Controllers", "ModuleId", c => c.Int(nullable: false,defaultValue:0));
        }
        
        public override void Down()
        {
            DropIndex("dbo.Modules", "UK_Module_Name");
            DropColumn("dbo.Controllers", "ModuleId");
            DropTable("dbo.Modules");
        }
    }
}
