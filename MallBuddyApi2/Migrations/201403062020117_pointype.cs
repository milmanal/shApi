namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Point3D", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Point3D", "Type");
        }
    }
}
