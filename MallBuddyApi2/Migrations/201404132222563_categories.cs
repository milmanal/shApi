namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryType", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Text", c => c.String());
            DropColumn("dbo.Categories", "CategoryType");
        }
    }
}
