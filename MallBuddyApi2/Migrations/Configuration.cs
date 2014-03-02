namespace MallBuddyApi2.Migrations
{
    using MallBuddyApi2.Models;
    using MallBuddyApi2.Models.existing;
    using Microsoft.SqlServer.Types;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.SqlServer;
    using System.Data.Entity.Validation;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    internal sealed class Configuration : DbMigrationsConfiguration<MallBuddyApi2.Models.ApplicationDbContext>
    {
        private const string JSON_PATH = @"C:\Users\mlmn\Downloads\012152834748849273080-0931412207.json";
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(MallBuddyApi2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //   System.Diagnostics.Debugger.Launch();

            //System.Diagnostics.Debugger.Break();
            SeedFromJson(context);

            //InsertGolfStore(context);
        }

        private void InsertGolfStore(MallBuddyApi2.Models.ApplicationDbContext context)
        {
            try
            {
                var golf = new Store
                {
                    Name = "GOLF",
                    Type = POI.POIType.STORE,
                    Anchor = new Point3D(),
                    Schedule = new List<Models.existing.OpeningHoursSpan>(),
                    Enabled = true,
                    WebsiteLink = "http://www.dizengof-center.co.il/GOLF.aspx",
                    ImageUrl = "http://www.dizengof-center.co.il/manage-pages/uploaded-files/a_03-07-2011-15-37-5.jpg",
                    Location = new Polygone(),
                    ContactDetails = new ContactDetails { PoiName = "GOLF", Phone1 = "03-5283163", Address = "www.golf.co.il" },
                    //Category = "Fashion",
                };
                var schedule1 = new List<OpeningHoursSpan>();
                var weekday = new OpeningHoursSpan { day = DayOfWeek.Sunday, from = 10, to = 2130 };
                var friday = new OpeningHoursSpan { day = DayOfWeek.Friday, from = 0930, to = 1530 };
                var saturday = new OpeningHoursSpan { day = DayOfWeek.Saturday, from = 20, to = 2230 };
                schedule1.Add(weekday); schedule1.Add(friday); schedule1.Add(saturday);
                golf.Schedule = schedule1;

                Polygone poly = new Polygone
                {
                    Accessible = true,
                    // Areas = new List<Area> { new Area { AreaID = "0401" }, new Area { AreaID = "0401A" } },
                    Wkt = "POLYGON ((34.775053932498516 32.07540339193275,34.77499915806167 32.07548404017222,34.77497984348338 32.07549149508553, 34.774953825764996 32.07549049106811,34.774935077805786 32.07547970303128,34.77491144862596 32.07545506831342,34.77490455271353 32.07544820193738, 34.77489280495869 32.075426482513976, 34.774889877466975 32.07541821355505,34.774886213457535 32.07540786423455,34.77488379926684 32.075401045136886,34.774871958289275 32.07534718017001,34.77487191400818 32.0752896316811,34.77492920795574 32.075324825765655,34.775053932498516 32.07540339193275,34.775053932498516 32.07540339193275))",
                    LocationG = DbGeometry.PolygonFromText("POLYGON ((34.775053932498516 32.07540339193275,34.77499915806167 32.07548404017222,34.77497984348338 32.07549149508553, 34.774953825764996 32.07549049106811,34.774935077805786 32.07547970303128,34.77491144862596 32.07545506831342,34.77490455271353 32.07544820193738, 34.77489280495869 32.075426482513976, 34.774889877466975 32.07541821355505,34.774886213457535 32.07540786423455,34.77488379926684 32.075401045136886,34.774871958289275 32.07534718017001,34.77487191400818 32.0752896316811,34.77492920795574 32.075324825765655,34.775053932498516 32.07540339193275,34.775053932498516 32.07540339193275))", 4326),
                    Points = new List<Point3D>()
                };
                var Point3D1 = new Point3D { Level = 2, Longitude = 34.775053932498516m, Latitude = 32.07540339193275m, Wkt = "POINT(34.775053932498516 32.07540339193275)", LocationG = DbGeometry.PointFromText("POINT(34.775053932498516 32.07540339193275)", 4326) };
                var Point3D2 = new Point3D { Level = 2, Longitude = 34.77499915806167m, Latitude = 32.07548404017222m, Wkt = "POINT(34.77499915806167 32.07548404017222)", LocationG = DbGeometry.PointFromText("POINT(34.77499915806167 32.07548404017222)", 4326) };
                var Point3D3 = new Point3D { Level = 2, Longitude = 34.77497984348338m, Latitude = 32.07549149508553m, Wkt = "POINT(34.77497984348338 32.07549149508553)", LocationG = DbGeometry.PointFromText("POINT(34.77497984348338 32.07549149508553)", 4326) };
                var Point3D4 = new Point3D { Level = 2, Longitude = 34.774953825764996m, Latitude = 32.07549049106811m, Wkt = "POINT(34.774953825764996 32.07549049106811)", LocationG = DbGeometry.PointFromText("POINT(34.774953825764996 32.07549049106811)", 4326) };
                poly.Points.Add(Point3D1); poly.Points.Add(Point3D2); poly.Points.Add(Point3D3); poly.Points.Add(Point3D4);
                golf.Location = poly;
                context.Stores.AddOrUpdate(golf);
                //context.
                SaveChanges(context);
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void SeedFromJson(ApplicationDbContext context)
        {
            try
            {
                Dictionary<string, Point3D> labeledPoints = new Dictionary<string, Point3D>();
                StringBuilder sb = new StringBuilder();
                string geojson = System.IO.File.ReadAllText(JSON_PATH);
                JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(geojson);
                List<JObject> level0 = new List<JObject>();
                List<Point3D> badPolygonsPoints = new List<Point3D>();
                List<JObject> polys = new List<JObject>();
                List<JObject> connetcors = new List<JObject>();
                int noareas = 0;

                foreach (JObject j in jObject["features"])
                {
                    if (j != null && j["properties"]["level"] != null && (j["properties"]["level"].ToString() == "0" | j["properties"]["level"].ToString() == "1"))
                    {
                        //level0.Add(j);
                        if (j != null && j["properties"]["connector"] != null && j["properties"]["connector"].ToString() == "true")
                            connetcors.Add(j);
                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "Point"
                              && j["properties"]["attrs"] != null && j["properties"]["attrs"]["name"] != null &&
                                j["properties"]["attrs"]["name"].ToString() != String.Empty)
                        {
                            //points.Add(j);
                            extractPoint(noareas, j, labeledPoints, badPolygonsPoints);

                        }
                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "Polygon")
                            polys.Add(j);
                        //extractPolygon(j, labeledPoints, context);
                    }
                }
                //foreach (JObject j in points)
                //    extractPoint(noareas, j, labeledPoints);
                //removeBadPolygons(badPolygonsPoints, polys);
                List<POI> poisToSave = new List<POI>();
                foreach (JObject j in polys)
                    extractPolygon(j, labeledPoints, context, badPolygonsPoints, poisToSave);
                foreach (JObject j in connetcors)
                    extractConnector(j, context);

                foreach (POI poi in poisToSave)
                {
                    //context.Polygones.AddOrUpdate(new Polygone[] { poi.Location });
                    ////SaveChanges(context);
                    ////try
                    ////{
                    ////    context.SaveChanges();
                    ////}
                    ////catch (Exception ex)
                    ////{
                    ////    ex = ex;
                    ////}
                    //try
                    //{
                    //    context.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex = ex;
                    //}
                    //foreach (POI poi in poisToSave)
                    //context.Polygones.Attach(poi.Location);
                    if (poi.Type == POI.POIType.STORE)
                    {
                        context.Stores.AddOrUpdate(new Store[] { (Store)poi });
                    }
                    else
                        context.POIs.AddOrUpdate(new POI[] { poi });
                    try
                    {

                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ex = ex;
                    }
                }

                //foreach (POI poi in poisToSave)
                //{
                //    if (poi.Type == POI.POIType.STORE)
                //        context.Stores.AddOrUpdate(new Store[] { (Store)poi });
                //    else
                //        context.POIs.AddOrUpdate(new POI[] { poi });
                //    SaveChanges(context);
                //}
                string s = "";
            }
            catch (Exception ex)
            {
                ex = ex;

            }

        }

        private void extractConnector(JObject j, ApplicationDbContext context)
        {
            //throw new NotImplementedException();
        }

        private static void extractPolygon(JObject feature, Dictionary<string, Point3D> labeledPoints, ApplicationDbContext context
            , List<Point3D> badPolygonsPoints, List<POI> poisToSave)
        {
            POI poi = new POI();
            poi.Type = null;
            poi.Modified = DateTime.Now;
            Polygone toReturn = new Polygone();
            toReturn.Points = new List<Point3D>();
            int level = int.Parse(feature["properties"]["level"].ToString());
            //bool isAccessible = Boolean.Parse(feature["properties"]["accessible"].ToString());
            toReturn.Level = level;
            StringBuilder wktsb = new StringBuilder("POLYGON ((");
            List<Point3D> labeledPointsFound = new List<Point3D>();
            foreach (JArray j in feature["geometry"]["coordinates"][0])
            {
                StringBuilder pointWkt = new StringBuilder("POINT (");
                string lon = Regex.Match(j.ToString(), "34\\.\\d+").Value;
                string lat = Regex.Match(j.ToString(), "32\\.\\d+").Value;
                wktsb.Append(lon + " " + lat + ",");
                pointWkt.Append(lon + " " + lat + ")");
                if (!toReturn.Points.Exists(Point3D => Point3D.Wkt == pointWkt.ToString()))
                {
                    if (labeledPoints.ContainsKey(pointWkt.ToString()))
                    {
                        labeledPointsFound.Add(labeledPoints[pointWkt.ToString()]);
                        toReturn.Points.Add(labeledPoints[pointWkt.ToString()]);
                        //break;
                    }
                    else
                    {
                        DbGeometry pointG = DbGeometry.PointFromText(pointWkt.ToString(), 4326);
                        Point3D point = new Point3D { Longitude = Decimal.Parse(lon), Latitude = Decimal.Parse(lat), Wkt = pointWkt.ToString(), LocationG = pointG, Level = level };
                        toReturn.Points.Add(point);
                    }
                }
            }
            wktsb.Remove(wktsb.Length - 1, 1);
            wktsb.Append("))");
            toReturn.Wkt = wktsb.ToString();
            try
            {
                DbGeometry polygone = DbGeometry.PolygonFromText(wktsb.ToString(), 4326);
                if (!polygone.IsValid)
                {
                    polygone = DbGeometry.FromText(SqlGeometry.STGeomFromText(new SqlChars(polygone.AsText()), 4326).MakeValid().STAsText().ToSqlString().ToString(), 4326);
                }

                foreach (Point3D point in badPolygonsPoints)
                    if (polygone.Contains(point.LocationG))
                        return;
                toReturn.LocationG = polygone;
            }
            catch (Exception ex)
            {
                ex = ex;
            }

            // polygon with no info
            if (labeledPointsFound.Count == 0)
                return;

            // polygon with more than one anchor point - check if it is 2 entrances
            if (labeledPointsFound.Count > 1)
            {
                if (labeledPointsFound[0].Areas.Count != labeledPointsFound[1].Areas.Count)
                    return;
                else
                    for (int i = 0; i < labeledPointsFound[0].Areas.Count; i++)
                        if (!labeledPointsFound[0].Areas[i].AreaID.Equals(labeledPointsFound[1].Areas[i].AreaID))
                            return;
            }
            Point3D labeledPoint = labeledPointsFound[0];
            if (labeledPoint != null)
            {
                toReturn.Areas = labeledPoint.Areas;
                if (labeledPoint.Areas != null && labeledPoint.Areas.Count > 0 && !labeledPoint.Areas[0].AreaID.StartsWith("9"))
                    poi.Type = POI.POIType.STORE;
                if (labeledPoint.Name.ToLower().Contains("atm"))
                    poi.Type = POI.POIType.ATM;
                if (labeledPoint.Name.ToLower().Contains("entrance"))
                    poi.Type = POI.POIType.ENTRANCE;
                if (labeledPoint.Name.ToLower().Contains("toilet") | labeledPoint.Name.ToLower().Contains("wc"))
                    poi.Type = POI.POIType.WC;
                if (labeledPoint.Name.ToLower().Contains("zone"))
                    poi.Type = POI.POIType.ZONE;
            }
            if (poi.Type != null)
            {
                switch (poi.Type)
                {
                    case POI.POIType.STORE:
                        {
                            poi = new Store
                            {
                                Type = poi.Type,
                                Anchor = labeledPoint,
                                Enabled = true
                                ,
                                Floor = level,
                                IsAccessible = labeledPoint.IsAccessible,
                                Name = labeledPoint.Name,
                                Name2 = labeledPoint.Name2,
                                Location = toReturn,
                                Entrances = new List<Point3D>()
                            };
                            foreach (Point3D entrancePoint in labeledPointsFound)
                                ((Store)poi).Entrances.Add(entrancePoint);
                            //context.Stores.AddOrUpdate(new Store[] { (Store)poi });
                            break;
                        }
                    default:
                        {
                            poi = new POI
                            {
                                Type = poi.Type,
                                Anchor = labeledPoint,
                                Enabled = true,
                                Name = labeledPoint.Name,
                                Location = toReturn
                            };
                            //poisToSave.Add(poi);
                            break;
                        }
                }
                poisToSave.Add(poi);
            }
            //return poi;
        }

        private static void extractPoint(int noareas, JObject feature, Dictionary<string, Point3D> labeledPoints, List<Point3D> badPolygonsPoints)
        {
            Point3D toReturn = new Point3D();
            toReturn.Level = int.Parse(feature["properties"]["level"].ToString());
            toReturn.IsAccessible = Boolean.Parse(feature["properties"]["accessible"].ToString());
            StringBuilder wktsb = new StringBuilder("POINT (");
            var j = feature["geometry"]["coordinates"];
            string lon = Regex.Match(j.ToString(), "34\\.\\d+").Value;
            string lat = Regex.Match(j.ToString(), "32\\.\\d+").Value;
            wktsb.Append(lon + " " + lat);
            wktsb.Append(")");
            toReturn.Wkt = wktsb.ToString();
            toReturn.LocationG = DbGeometry.PointFromText(wktsb.ToString(), 4326);
            toReturn.Name = feature["properties"]["attrs"]["name"].ToString();
            toReturn.Latitude = Decimal.Parse(lat);
            toReturn.Longitude = Decimal.Parse(lon);
            if (feature["properties"]["attrs"]["Name2"] != null)
                toReturn.Name2 = feature["properties"]["attrs"]["Name2"].ToString();
            toReturn.Areas = new List<Area>();
            if (feature["properties"]["attrs"]["AreaID"] != null)
            {
                Area area = new Area(feature["properties"]["attrs"]["AreaID"].ToString());
                toReturn.Areas.Add(area);
                int index = 2;
                while (feature["properties"]["attrs"]["AreaID" + index] != null)
                {
                    toReturn.Areas.Add(new Area(feature["properties"]["attrs"]["AreaID" + index++].ToString()));
                }

            }
            else
            {
                noareas++;
            }
            if (toReturn.Name.ToUpper().Contains("PASSAGES") | toReturn.Name.ToUpper().Contains("ROADWALK"))
                badPolygonsPoints.Add(toReturn);
            else
                labeledPoints.Add(toReturn.Wkt, toReturn);
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
    }
}
