namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaID = c.String(nullable: false, maxLength: 128),
                        Polygone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AreaID)
                .ForeignKey("dbo.Polygones", t => t.Polygone_Id)
                .Index(t => t.Polygone_Id);
            
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
                "dbo.LineStrings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        LocationG = c.Geometry(),
                        Wkt = c.String(),
                        Name = c.String(),
                        IsAccessible = c.Boolean(nullable: false),
                        connectorType = c.Int(nullable: false),
                        BiDirectional = c.Boolean(nullable: false),
                        Distance = c.Double(nullable: false),
                        Source_Id = c.Long(),
                        Target_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Point3D", t => t.Source_Id)
                .ForeignKey("dbo.Point3D", t => t.Target_Id)
                .Index(t => t.Source_Id)
                .Index(t => t.Target_Id);
            
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
                        Type = c.Int(nullable: false),
                        POI_DbID = c.Long(),
                        Polygone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POIs", t => t.POI_DbID)
                .ForeignKey("dbo.Polygones", t => t.Polygone_Id)
                .Index(t => t.POI_DbID)
                .Index(t => t.Polygone_Id);
            
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
                        Modified = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Store_DbID = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POIs", t => t.Store_DbID)
                .Index(t => t.Store_DbID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpeningHoursSpans", "Store_DbID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "ContactDetails_PoiName", "dbo.ContactDetails");
            DropForeignKey("dbo.Categories", "Store_DbID", "dbo.POIs");
            DropForeignKey("dbo.OpeningHoursSpans", "Entrance_DbID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "Location_Id", "dbo.Polygones");
            DropForeignKey("dbo.Point3D", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.Images", "POI_DbID", "dbo.POIs");
            DropForeignKey("dbo.Point3D", "POI_DbID", "dbo.POIs");
            DropForeignKey("dbo.LineStrings", "Target_Id", "dbo.Point3D");
            DropForeignKey("dbo.LineStrings", "Source_Id", "dbo.Point3D");
            DropIndex("dbo.OpeningHoursSpans", new[] { "Store_DbID" });
            DropIndex("dbo.POIs", new[] { "ContactDetails_PoiName" });
            DropIndex("dbo.Categories", new[] { "Store_DbID" });
            DropIndex("dbo.OpeningHoursSpans", new[] { "Entrance_DbID" });
            DropIndex("dbo.POIs", new[] { "Location_Id" });
            DropIndex("dbo.Point3D", new[] { "Polygone_Id" });
            DropIndex("dbo.Areas", new[] { "Polygone_Id" });
            DropIndex("dbo.Images", new[] { "POI_DbID" });
            DropIndex("dbo.Point3D", new[] { "POI_DbID" });
            DropIndex("dbo.LineStrings", new[] { "Target_Id" });
            DropIndex("dbo.LineStrings", new[] { "Source_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.OpeningHoursSpans");
            DropTable("dbo.Polygones");
            DropTable("dbo.POIs");
            DropTable("dbo.Point3D");
            DropTable("dbo.LineStrings");
            DropTable("dbo.Images");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Areas");
        }
    }
}
