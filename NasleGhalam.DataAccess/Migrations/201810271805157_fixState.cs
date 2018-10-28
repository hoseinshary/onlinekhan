namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixState : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EducationTrees", name: "LookupId_EducationTree", newName: "LookupId_EducationTreeState");
            RenameIndex(table: "dbo.EducationTrees", name: "IX_LookupId_EducationTree", newName: "IX_LookupId_EducationTreeState");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.EducationTrees", name: "IX_LookupId_EducationTreeState", newName: "IX_LookupId_EducationTree");
            RenameColumn(table: "dbo.EducationTrees", name: "LookupId_EducationTreeState", newName: "LookupId_EducationTree");
        }
    }
}
