namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withDT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POIs", "Modified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POIs", "Modified");
        }
    }
}
