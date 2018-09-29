namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class nullableParentTopic : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            AlterColumn("dbo.Topics", "ParentTopicId", c => c.Int());
            CreateIndex("dbo.Topics", "ParentTopicId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            AlterColumn("dbo.Topics", "ParentTopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "ParentTopicId");
        }
    }
}
