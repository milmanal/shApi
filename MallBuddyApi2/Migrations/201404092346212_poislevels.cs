namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poislevels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POIs", "Level", c => c.Int(nullable: false));
            DropColumn("dbo.POIs", "Floor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POIs", "Floor", c => c.Int());
            DropColumn("dbo.POIs", "Level");
        }
    }
}
