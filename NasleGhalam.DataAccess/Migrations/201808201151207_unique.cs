namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique : DbMigration
    {
        public override void Up()
        {
            //RenameIndex(table: "dbo.Roles", name: "IX_Name", newName: "");
            CreateIndex("dbo.Roles", "Name", unique: true, name: "UK_Role_Name");
            CreateIndex("dbo.EducationBooks", "Name", unique: true, name: "UK_EducationBook_Name");
            CreateIndex("dbo.EducationGroups", "Name", unique: true, name: "UK_EducationGroup_Name");
            CreateIndex("dbo.EducationYears", "Name", unique: true, name: "UK_EducationYear_Name");
            CreateIndex("dbo.Users", "NationalNo", unique: true, name: "UK_User_NationalNo");
            CreateIndex("dbo.Provinces", "Name", unique: true, name: "UK_Province_Name");
            CreateIndex("dbo.Tags", "Name", unique: true, name: "UK_Tag_Name");
            CreateIndex("dbo.Topics", "Title", unique: true, name: "UK_Topic_Name");
            CreateIndex("dbo.UniversityBranches", "Name", unique: true, name: "UK_UniversityBranch_Name");
            CreateIndex("dbo.Publishers", "Name", unique: true, name: "UK_Publisher_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Publishers", "UK_Publisher_Name");
            DropIndex("dbo.UniversityBranches", "UK_UniversityBranch_Name");
            DropIndex("dbo.Topics", "UK_Topic_Name");
            DropIndex("dbo.Tags", "UK_Tag_Name");
            DropIndex("dbo.Provinces", "UK_Province_Name");
            DropIndex("dbo.Users", "UK_User_NationalNo");
            DropIndex("dbo.EducationYears", "UK_EducationYear_Name");
            DropIndex("dbo.EducationGroups", "UK_EducationGroup_Name");
            DropIndex("dbo.EducationBooks", "UK_EducationBook_Name");
            DropIndex("dbo.Roles", "UK_Role_Name");
            //RenameIndex(table: "dbo.Roles", name: "UK_Role_Name", newName: "IX_Name");
        }
    }
}
