namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AreaID = c.String(),
                        Point3D_Id = c.Long(),
                        Polygone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Point3D", t => t.Point3D_Id)
                .ForeignKey("dbo.Polygones", t => t.Polygone_Id)
                .Index(t => t.Point3D_Id)
                .Index(t => t.Polygone_Id);
            
            CreateTable(
                "dbo.Connectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartLevel = c.Int(nullable: false),
                        EndLevel = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Anchor_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Point3D", t => t.Anchor_Id)
                .Index(t => t.Anchor_Id);
            
            CreateTable(
                "dbo.Point3D",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 15),
                        Latitude = c.Decimal(nullable: false, precision: 17, scale: 15),
                        Level = c.Int(nullable: false),
                        LocationG = c.Geometry(),
                        Wkt = c.String(),
                        Name = c.String(),
                        Name2 = c.String(),
                        IsAccessible = c.Boolean(nullable: false),
                        Polygone_Id = c.Int(),
                        Store_DbID = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polygones", t => t.Polygone_Id)
                .ForeignKey("dbo.POIs", t => t.Store_DbID)
                .Index(t => t.Polygone_Id)
                .Index(t => t.Store_DbID);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        PoiName = c.String(nullable: false, maxLength: 128),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Contactname = c.String(),
                    })
                .PrimaryKey(t => t.PoiName);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                        POI_DbID = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POIs", t => t.POI_DbID)
                .Index(t => t.POI_DbID);
            
            CreateTable(
                "dbo.POIs",
                c => new
                    {
                        DbID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsWalkable = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Type = c.Int(),
                        Enabled = c.Boolean(nullable: false),
                        gateID = c.String(),
                        Floor = c.Int(),
                        WebsiteLink = c.String(),
                        LogoUrl = c.String(),
                        IsAccessible = c.Boolean(),
                        Name2 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Location_Id = c.Int(),
                        ContactDetails_PoiName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DbID)
                .ForeignKey("dbo.Polygones", t => t.Location_Id)
                .ForeignKey("dbo.ContactDetails", t => t.ContactDetails_PoiName)
                .Index(t => t.Location_Id)
                .Index(t => t.ContactDetails_PoiName);
            
            CreateTable(
                "dbo.Polygones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Wkt = c.String(),
                        Level = c.Int(nullable: false),
                        LocationG = c.Geometry(),
                        Accessible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OpeningHoursSpans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        day = c.Int(nullable: false),
                        from = c.Int(nullable: false),
                        to = c.Int(nullable: false),
                        Entrance_DbID = c.Long(),
                        Store_DbID = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POIs", t => t.Entrance_DbID)
                .ForeignKey("dbo.POIs", t => t.Store_DbID)
                .Index(t => t.Entrance_DbID)
                .Index(t => t.Store_DbID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        Store_DbID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.POIs", t => t.Store_DbID)
                .Index(t => t.Store_DbID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpeningHoursSpans", "Store_DbID", "dbo.POIs");
            DropForeignKey("dbo.Point3D", "Store_DbID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "ContactDetails_PoiName", "dbo.ContactDetails");
            DropForeignKey("dbo.Categories", "Store_DbID", "dbo.POIs");
            DropForeignKey("dbo.OpeningHoursSpans", "Entrance_DbID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "Location_Id", "dbo.Polygones");
            DropForeignKey("dbo.Point3D", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.Images", "POI_DbID", "dbo.POIs");
            DropForeignKey("dbo.Connectors", "Anchor_Id", "dbo.Point3D");
            DropForeignKey("dbo.Areas", "Point3D_Id", "dbo.Point3D");
            DropIndex("dbo.OpeningHoursSpans", new[] { "Store_DbID" });
            DropIndex("dbo.Point3D", new[] { "Store_DbID" });
            DropIndex("dbo.POIs", new[] { "ContactDetails_PoiName" });
            DropIndex("dbo.Categories", new[] { "Store_DbID" });
            DropIndex("dbo.OpeningHoursSpans", new[] { "Entrance_DbID" });
            DropIndex("dbo.POIs", new[] { "Location_Id" });
            DropIndex("dbo.Point3D", new[] { "Polygone_Id" });
            DropIndex("dbo.Areas", new[] { "Polygone_Id" });
            DropIndex("dbo.Images", new[] { "POI_DbID" });
            DropIndex("dbo.Connectors", new[] { "Anchor_Id" });
            DropIndex("dbo.Areas", new[] { "Point3D_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.OpeningHoursSpans");
            DropTable("dbo.Polygones");
            DropTable("dbo.POIs");
            DropTable("dbo.Images");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Point3D");
            DropTable("dbo.Connectors");
            DropTable("dbo.Areas");
        }
    }
}
