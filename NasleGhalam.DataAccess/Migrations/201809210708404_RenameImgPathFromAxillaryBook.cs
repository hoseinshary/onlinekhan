namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RenameImgPathFromAxillaryBook : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.AxillaryBooks", "ImgPath", "ImgName");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.AxillaryBooks", "ImgName", "ImgPath");
        }
    }
}
