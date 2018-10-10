namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lesson_fix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Topics", "UK_Topic_Name");

            AddColumn("dbo.Lessons", "LookupId_Nezam", c => c.Int(nullable: false, defaultValue: 1028));
            
            CreateIndex("dbo.Lessons", "LookupId_Nezam");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lessons", new[] { "LookupId_Nezam" });
            AlterColumn("dbo.Lessons", "LookupId_Nezam", c => c.Int());
            RenameColumn(table: "dbo.Lessons", name: "LookupId_Nezam", newName: "Lookup_Nezam_Id");
            AddColumn("dbo.Lessons", "LookupId_Nezam", c => c.Int(nullable: false, defaultValue: 1028));
            CreateIndex("dbo.Lessons", "Lookup_Nezam_Id");
            CreateIndex("dbo.Topics", "Title", unique: true, name: "UK_Topic_Name");
        }
    }
}
