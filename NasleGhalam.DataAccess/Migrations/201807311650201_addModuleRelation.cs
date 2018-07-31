namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addModuleRelation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Controllers", "ModuleId");
            AddForeignKey("dbo.Controllers", "ModuleId", "dbo.Modules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Controllers", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Controllers", new[] { "ModuleId" });
        }
    }
}
