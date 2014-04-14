namespace MallBuddyApi2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class polypoi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.Point3D", "Polygone_Id", "dbo.Polygones");
            DropForeignKey("dbo.POIs", "Location_Id", "dbo.Polygones");
            DropIndex("dbo.Areas", new[] { "Polygone_Id" });
            DropIndex("dbo.Point3D", new[] { "Polygone_Id" });
            DropIndex("dbo.POIs", new[] { "Location_Id" });
            RenameColumn(table: "dbo.Areas", name: "Polygone_Id", newName: "Polygone_PoiId");
            RenameColumn(table: "dbo.Point3D", name: "Polygone_Id", newName: "Polygone_PoiId");
            AddColumn("dbo.Polygones", "PoiId", c => c.Long(nullable: false));
            AlterColumn("dbo.Areas", "Polygone_PoiId", c => c.Long());
            AlterColumn("dbo.Point3D", "Polygone_PoiId", c => c.Long());
            DropPrimaryKey("dbo.Polygones");
            AddPrimaryKey("dbo.Polygones", "PoiId");
            CreateIndex("dbo.Areas", "Polygone_PoiId");
            CreateIndex("dbo.Point3D", "Polygone_PoiId");
            CreateIndex("dbo.Polygones", "PoiId");
            AddForeignKey("dbo.Areas", "Polygone_PoiId", "dbo.Polygones", "PoiId");
            AddForeignKey("dbo.Point3D", "Polygone_PoiId", "dbo.Polygones", "PoiId");
            AddForeignKey("dbo.Polygones", "PoiId", "dbo.POIs", "DbID");
            DropColumn("dbo.POIs", "Location_Id");
            DropColumn("dbo.Polygones", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Polygones", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.POIs", "Location_Id", c => c.Int());
            DropForeignKey("dbo.Polygones", "PoiId", "dbo.POIs");
            DropForeignKey("dbo.Point3D", "Polygone_PoiId", "dbo.Polygones");
            DropForeignKey("dbo.Areas", "Polygone_PoiId", "dbo.Polygones");
            DropIndex("dbo.Polygones", new[] { "PoiId" });
            DropIndex("dbo.Point3D", new[] { "Polygone_PoiId" });
            DropIndex("dbo.Areas", new[] { "Polygone_PoiId" });
            DropPrimaryKey("dbo.Polygones");
            AddPrimaryKey("dbo.Polygones", "Id");
            AlterColumn("dbo.Point3D", "Polygone_PoiId", c => c.Int());
            AlterColumn("dbo.Areas", "Polygone_PoiId", c => c.Int());
            DropColumn("dbo.Polygones", "PoiId");
            RenameColumn(table: "dbo.Point3D", name: "Polygone_PoiId", newName: "Polygone_Id");
            RenameColumn(table: "dbo.Areas", name: "Polygone_PoiId", newName: "Polygone_Id");
            CreateIndex("dbo.POIs", "Location_Id");
            CreateIndex("dbo.Point3D", "Polygone_Id");
            CreateIndex("dbo.Areas", "Polygone_Id");
            AddForeignKey("dbo.POIs", "Location_Id", "dbo.Polygones", "Id");
            AddForeignKey("dbo.Point3D", "Polygone_Id", "dbo.Polygones", "Id");
            AddForeignKey("dbo.Areas", "Polygone_Id", "dbo.Polygones", "Id");
        }
    }
}
