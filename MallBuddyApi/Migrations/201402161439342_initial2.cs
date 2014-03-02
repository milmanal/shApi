namespace MallBuddyApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.POIs", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POIs", "Id", c => c.String());
        }
    }
}
