namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateRole : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EducationTrees", name: "LookupId_EducationTreeState", newName: "LookupId_EducationTree");
            RenameIndex(table: "dbo.EducationTrees", name: "IX_LookupId_EducationTreeState", newName: "IX_LookupId_EducationTree");
            AddColumn("dbo.Roles", "UserType", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.Roles", "UserType");
            RenameIndex(table: "dbo.EducationTrees", name: "IX_LookupId_EducationTree", newName: "IX_LookupId_EducationTreeState");
            RenameColumn(table: "dbo.EducationTrees", name: "LookupId_EducationTree", newName: "LookupId_EducationTreeState");
        }
    }
}
