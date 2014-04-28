namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entrances : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "POI_DbID", "dbo.POIs");
            DropIndex("dbo.Images", new[] { "POI_DbID" });
            DropColumn("dbo.Images", "POI_DbID");
            DropColumn("dbo.POIs", "IsWalkable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POIs", "IsWalkable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Images", "POI_DbID", c => c.Long());
            CreateIndex("dbo.Images", "POI_DbID");
            AddForeignKey("dbo.Images", "POI_DbID", "dbo.POIs", "DbID");
        }
    }
}
