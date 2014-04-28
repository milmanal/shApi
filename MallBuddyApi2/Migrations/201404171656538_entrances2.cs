namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entrances2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
