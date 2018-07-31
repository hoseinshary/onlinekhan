namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class EducationSubGroupRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EducationSubGroups", "EducationGroupId", "dbo.EducationGroups");
            AddForeignKey("dbo.EducationSubGroups", "EducationGroupId", "dbo.EducationGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EducationSubGroups", "EducationGroupId", "dbo.EducationGroups");
            AddForeignKey("dbo.EducationSubGroups", "EducationGroupId", "dbo.EducationGroups", "Id", cascadeDelete: true);
        }
    }
}
