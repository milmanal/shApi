namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class areas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "Polygone_Id", c => c.Int());
            CreateIndex("dbo.Areas", "Polygone_Id");
            AddForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones");
            DropIndex("dbo.Areas", new[] { "Polygone_Id" });
            DropColumn("dbo.Areas", "Polygone_Id");
        }
    }
}
