namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Majorlists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentMajorList_Major",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        StudentMajorListId = c.Int(nullable: false),
                        MajorsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Majors", t => t.MajorsId)
                .ForeignKey("dbo.StudentMajorlists", t => t.StudentMajorListId)
                .Index(t => t.StudentMajorListId)
                .Index(t => t.MajorsId);
            
          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentMajorList_Major", "StudentMajorListId", "dbo.StudentMajorlists");
            DropForeignKey("dbo.StudentMajorList_Major", "MajorsId", "dbo.Majors");
            DropIndex("dbo.StudentMajorList_Major", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorList_Major", new[] { "StudentMajorListId" });
           
            DropTable("dbo.StudentMajorList_Major");
        }
    }
}
