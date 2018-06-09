namespace NasleGhalam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId, unique: true);
            
            AddColumn("dbo.Users", "NationalNo", c => c.String(maxLength: 10));
            AddColumn("dbo.Users", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Phone", c => c.String(maxLength: 8));
            AddColumn("dbo.Users", "Mobile", c => c.String(maxLength: 10));
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropColumn("dbo.Users", "CityId");
            DropColumn("dbo.Users", "LastLogin");
            DropColumn("dbo.Users", "Mobile");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Gender");
            DropColumn("dbo.Users", "NationalNo");
            DropTable("dbo.Students");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
        }
    }
}
