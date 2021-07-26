namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentMajors : DbMigration
    {
        public override void Up()
        {
           
            
            CreateTable(
                "dbo.StudentMajorlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        UserId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
           
            
            CreateTable(
                "dbo.StudentMajorlist_Majors",
                c => new
                    {
                        StudentMajorlistId = c.Int(nullable: false),
                        MajorsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentMajorlistId, t.MajorsId })
                .ForeignKey("dbo.StudentMajorlists", t => t.StudentMajorlistId)
                .ForeignKey("dbo.Majors", t => t.MajorsId)
                .Index(t => t.StudentMajorlistId)
                .Index(t => t.MajorsId);
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentMajorlists", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentMajorlist_Majors", "MajorsId", "dbo.Majors");
            DropForeignKey("dbo.StudentMajorlist_Majors", "StudentMajorlistId", "dbo.StudentMajorlists");
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "MajorsId" });
            DropIndex("dbo.StudentMajorlist_Majors", new[] { "StudentMajorlistId" });
            DropIndex("dbo.StudentMajorlists", new[] { "UserId" });
            DropTable("dbo.StudentMajorlist_Majors");
            DropTable("dbo.StudentMajorlists");
        }
    }
}
