namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lesson_historyEducaiton_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UniversityBranches_HistoryEducations", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.UniversityBranches_HistoryEducations", "UniversityBranchId", "dbo.UniversityBranches");
            DropIndex("dbo.UniversityBranches_HistoryEducations", new[] { "UniversityBranchId" });
            DropIndex("dbo.UniversityBranches_HistoryEducations", new[] { "HistoryEducationId" });
            CreateTable(
                "dbo.HistoryEducations_Cities",
                c => new
                    {
                        HistoryEducationId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryEducationId, t.CityId })
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.HistoryEducationId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.HistoryEducations_UniversityBranchs",
                c => new
                    {
                        HistoryEducationId = c.Int(nullable: false),
                        UniversityBranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryEducationId, t.UniversityBranchId })
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.UniversityBranches", t => t.UniversityBranchId)
                .Index(t => t.HistoryEducationId)
                .Index(t => t.UniversityBranchId);
            
            AddColumn("dbo.HistoryEducations", "RankGoal", c => c.Int(nullable: false));
            AddColumn("dbo.UniversityBranches", "Balance1Low", c => c.Int(nullable: false));
            AddColumn("dbo.UniversityBranches", "Balance1High", c => c.Int(nullable: false));
            AddColumn("dbo.UniversityBranches", "Balance2Low", c => c.Int(nullable: false));
            AddColumn("dbo.UniversityBranches", "Balance2High", c => c.Int(nullable: false));
            AddColumn("dbo.Lessons", "LookupId_Nezam", c => c.Int(nullable: false));
            AddColumn("dbo.Lessons", "Lookup_Nezam_Id", c => c.Int());
            CreateIndex("dbo.Lessons", "Lookup_Nezam_Id");
            AddForeignKey("dbo.Lessons", "Lookup_Nezam_Id", "dbo.Lookups", "Id");
            DropColumn("dbo.UniversityBranches", "Balance");
            DropTable("dbo.UniversityBranches_HistoryEducations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UniversityBranches_HistoryEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniversityBranchId = c.Int(nullable: false),
                        HistoryEducationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UniversityBranches", "Balance", c => c.Int(nullable: false));
            DropForeignKey("dbo.Lessons", "Lookup_Nezam_Id", "dbo.Lookups");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "UniversityBranchId", "dbo.UniversityBranches");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.HistoryEducations_Cities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.HistoryEducations_Cities", "HistoryEducationId", "dbo.HistoryEducations");
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "UniversityBranchId" });
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "HistoryEducationId" });
            DropIndex("dbo.HistoryEducations_Cities", new[] { "CityId" });
            DropIndex("dbo.HistoryEducations_Cities", new[] { "HistoryEducationId" });
            DropIndex("dbo.Lessons", new[] { "Lookup_Nezam_Id" });
            DropColumn("dbo.Lessons", "Lookup_Nezam_Id");
            DropColumn("dbo.Lessons", "LookupId_Nezam");
            DropColumn("dbo.UniversityBranches", "Balance2High");
            DropColumn("dbo.UniversityBranches", "Balance2Low");
            DropColumn("dbo.UniversityBranches", "Balance1High");
            DropColumn("dbo.UniversityBranches", "Balance1Low");
            DropColumn("dbo.HistoryEducations", "RankGoal");
            DropTable("dbo.HistoryEducations_UniversityBranchs");
            DropTable("dbo.HistoryEducations_Cities");
            CreateIndex("dbo.UniversityBranches_HistoryEducations", "HistoryEducationId");
            CreateIndex("dbo.UniversityBranches_HistoryEducations", "UniversityBranchId");
            AddForeignKey("dbo.UniversityBranches_HistoryEducations", "UniversityBranchId", "dbo.UniversityBranches", "Id");
            AddForeignKey("dbo.UniversityBranches_HistoryEducations", "HistoryEducationId", "dbo.HistoryEducations", "Id");
        }
    }
}
