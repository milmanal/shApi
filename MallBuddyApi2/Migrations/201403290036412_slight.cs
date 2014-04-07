namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slight : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpeningHoursSpans", "Entrance_DbID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "ContactDetails_PoiName", "dbo.ContactDetails");
            DropForeignKey("dbo.OpeningHoursSpans", "Store_DbID", "dbo.POIs");
            DropIndex("dbo.OpeningHoursSpans", new[] { "Entrance_DbID" });
            DropIndex("dbo.POIs", new[] { "ContactDetails_PoiName" });
            DropIndex("dbo.OpeningHoursSpans", new[] { "Store_DbID" });
            AddColumn("dbo.POIs", "Phone", c => c.String());
            AlterColumn("dbo.OpeningHoursSpans", "from", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OpeningHoursSpans", "to", c => c.DateTime(nullable: false));
            DropColumn("dbo.POIs", "ContactDetails_PoiName");
            DropColumn("dbo.OpeningHoursSpans", "Entrance_DbID");
            DropColumn("dbo.OpeningHoursSpans", "Store_DbID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OpeningHoursSpans", "Store_DbID", c => c.Long());
            AddColumn("dbo.OpeningHoursSpans", "Entrance_DbID", c => c.Long());
            AddColumn("dbo.POIs", "ContactDetails_PoiName", c => c.String(maxLength: 128));
            AlterColumn("dbo.OpeningHoursSpans", "to", c => c.Int(nullable: false));
            AlterColumn("dbo.OpeningHoursSpans", "from", c => c.Int(nullable: false));
            DropColumn("dbo.POIs", "Phone");
            CreateIndex("dbo.OpeningHoursSpans", "Store_DbID");
            CreateIndex("dbo.POIs", "ContactDetails_PoiName");
            CreateIndex("dbo.OpeningHoursSpans", "Entrance_DbID");
            AddForeignKey("dbo.OpeningHoursSpans", "Store_DbID", "dbo.POIs", "DbID");
            AddForeignKey("dbo.POIs", "ContactDetails_PoiName", "dbo.ContactDetails", "PoiName");
            AddForeignKey("dbo.OpeningHoursSpans", "Entrance_DbID", "dbo.POIs", "DbID");
        }
    }
}
