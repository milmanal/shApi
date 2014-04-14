namespace MallBuddyApi2.Migrations
{
    using MallBuddyApi2.Models;
    using MallBuddyApi2.Models.existing;
    using MallBuddyApi2.Utils;
    using Microsoft.SqlServer.Types;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.SqlServer;
    using System.Data.Entity.Validation;
    using System.Data.SqlTypes;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using CsvHelper;

    internal sealed class Configuration : DbMigrationsConfiguration<MallBuddyApi2.Models.ApplicationDbContext>
    {
        private const string JSON_PATH = @"F:\Users\mlmn\Documents\Indoor\indoorIO\012152834748849273080-0240009142.json";
        private const string DC_STATS_PATH = @"F:\Users\mlmn\Documents\Indoor\Maps\Dizengoff Center\CMS\dc-poisEG0604.csv";
        private const string DC_STATS_DIR = @"F:\Users\mlmn\Documents\Indoor\Maps\Dizengoff Center\CMS\";
        
        private const string JSON_DIR_PATH = @"F:\Users\mlmn\Documents\Indoor\indoorIO";
        ApplicationDbContext dcImportContext;// = new ApplicationDbContext("DCImport");

        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            //dcImportContext = new ApplicationDbContext("DCImport");
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
            SeedFromJson(new ApplicationDbContext("DefaultConnection"));
            //SeedFromJson(new ApplicationDbContext("Indoor"));
            //ReadDCData(new ApplicationDbContext("DCImport2"));
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
                    //Schedule = new List<Models.existing.OpeningHoursSpan>(),
                    Enabled = true,
                    WebsiteLink = "http://www.dizengof-center.co.il/GOLF.aspx",
                    ImageUrl = "http://www.dizengof-center.co.il/manage-pages/uploaded-files/a_03-07-2011-15-37-5.jpg",
                    Location = new Polygone(),
                    //ContactDetails = new ContactDetails { PoiName = "GOLF", Phone1 = "03-5283163", Address = "www.golf.co.il" },
                    //Category = "Fashion",
                };
                var schedule1 = new List<OpeningHoursSpan>();
                //var weekday = new OpeningHoursSpan { day = DayOfWeek.Sunday, from = 10, to = 2130 };
                //var friday = new OpeningHoursSpan { day = DayOfWeek.Friday, from = 0930, to = 1530 };
                //var saturday = new OpeningHoursSpan { day = DayOfWeek.Saturday, from = 20, to = 2230 };
                //schedule1.Add(weekday); schedule1.Add(friday); schedule1.Add(saturday);
                //golf.Schedule = schedule1;

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

        private void ReadDCData(ApplicationDbContext context)
        {
            Dictionary<string, string> categoriesMappings = new Dictionary<string, string>();
            string[] enumNames = Enum.GetNames(typeof(Category.StoreCategory));
            Category[] allCategories = new Category[enumNames.Length];
            for (int i = 0; i < Category.HebrewCategoriesCenter.Length; i++)
            {
                allCategories[i] = new Category(Category.HebrewCategoriesCenter[i]);
                categoriesMappings.Add(Category.HebrewCategoriesCenter[i], enumNames[i]);
            }
            Dictionary<string, POI.POIType> typesMappings = new Dictionary<string, POI.POIType>();
            enumNames = Enum.GetNames(typeof(POI.POIType));
            for (int i = 0; i < POI.HebrewTypeMappings.Length; i++)
            {
                typesMappings.Add(POI.HebrewTypeMappings[i], (POI.POIType)(Enum.Parse(typeof(POI.POIType), enumNames[i])));
            }
            var directory = new DirectoryInfo(DC_STATS_DIR);
            var myFile = directory.GetFiles("*.csv")
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();
            using (var fileReader = File.OpenText(myFile.FullName))
            using (var csvReader = new CsvHelper.CsvReader(fileReader))
            {
                //var map = csvReader.Configuration.AutoMap<POI>();
                //map.
                while (csvReader.Read())
                {
                    POI poi = null;
                    var type = POI.POIType.NONE;
                    string typeString;
                    if (csvReader.TryGetField("Type", out typeString) && typesMappings.ContainsKey(typeString))
                    {
                        type = typesMappings[typeString];
                    }
                    if (type == POI.POIType.NONE)
                        continue;
                    if (type == POI.POIType.STORE)
                    {
                        poi = new Store();
                        ((Store)poi).Name2 = csvReader.GetField("Name2").Trim();
                        ((Store)poi).LogoUrl = csvReader.GetField("LogoUrl").Trim();
                        ((Store)poi).WebsiteLink = csvReader.GetField("WebsiteLink").Trim();
                        ((Store)poi).IsAccessible = csvReader.GetField("Accessibility").Trim().Contains("כן");
                        List<Category> categories = new List<Category>();
                        string[] categoriesStrings = csvReader.GetField("Categories").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Category toAdd = null;
                        foreach (String cat in categoriesStrings)
                        {

                            try
                            {
                                toAdd = new Category(categoriesMappings[cat.Trim()]);
                                categories.Add(toAdd);
                            }
                            catch (Exception ex)
                            {
                                ex = ex;
                            }
                            //Category found = allCategories.SingleOrDefault(x=>x.Text == cat.Trim());
                            //if (found!=null)
                              //  categories.Add(found);
                        }
                        ((Store)poi).Categories = categories;
                        ((Store)poi).Phone = csvReader.GetField("Phone").Trim();
                        ((Store)poi).Schedule = GetTimeTables(csvReader);

                    }
                    else if (type == POI.POIType.ENTRANCE)
                    {
                        poi = new Entrance();
                        ((Entrance)poi).Schedule = GetTimeTables(csvReader);
                        poi.Name = csvReader.GetField("Name").Trim();
                        ((Entrance)poi).gateID = Regex.Match(poi.Name, "\\d+").Value;
                    }
                    else
                        poi = new POI();
                    poi.Type = type;
                    poi.Name = csvReader.GetField("Name").Trim();
                    string t = null;
                    csvReader.TryGetField("ImageUrl", out t);
                    poi.ImageUrl = t;
                    poi.Modified = DateTime.Now;
                    poi.Location = new Polygone();
                    poi.Location.Areas = new List<Area>();
                    string[] areasStrings = csvReader.GetField("Areas").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string area in areasStrings)
                        poi.Location.Areas.Add(new Area(area.Trim()));
                    poi.Location.POI = poi;
                    context.POIs.Add(poi);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        ex = ex;
                    }
                }
            }
        }


        private List<OperationHours> GetTimeTables(CsvReader csvReader)
        {
            TimeSpan? weekdayfrom = null;// = SqlDateTime.MinValue;
            DateTime weekdayfromdt;
            //DateTime.TryParseExact(csvReader.GetField("WeekdayOpen"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out weekdayfromdt);
            if (DateTime.TryParseExact(csvReader.GetField("WeekdayOpen"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out weekdayfromdt))
                weekdayfrom = weekdayfromdt.TimeOfDay;
            TimeSpan? weekdayto = null;
            DateTime weekdaytodt;
            //DateTime.TryParseExact(csvReader.GetField("WeekdayClose"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out weekdaytodt);
            if (DateTime.TryParseExact(csvReader.GetField("WeekdayClose"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out weekdaytodt))
                weekdayto = weekdaytodt.TimeOfDay;

            OperationHours sunday = new OperationHours { day = DayOfWeek.Sunday, from = weekdayfrom, to = weekdayto };
            OperationHours monday = new OperationHours { day = DayOfWeek.Monday, from = weekdayfrom, to = weekdayto };
            OperationHours tuesday = new OperationHours { day = DayOfWeek.Tuesday, from = weekdayfrom, to = weekdayto };
            OperationHours wednesday = new OperationHours { day = DayOfWeek.Wednesday, from = weekdayfrom, to = weekdayto };
            OperationHours thursday = new OperationHours { day = DayOfWeek.Thursday, from = weekdayfrom, to = weekdayto };
            TimeSpan? fridayfrom = null;
            DateTime fridayfromdt;
            //DateTime.TryParseExact(csvReader.GetField("FridayOpen"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out fridayfromdt);
            if (DateTime.TryParseExact(csvReader.GetField("FridayOpen"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out fridayfromdt))
                fridayfrom = fridayfromdt.TimeOfDay;
            TimeSpan? fridayto = null;
            DateTime fridaytodt;
            //DateTime.TryParseExact(csvReader.GetField("FridayClose"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out fridaytodt);
            if (DateTime.TryParseExact(csvReader.GetField("FridayClose"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out fridaytodt))
                fridayto = fridaytodt.TimeOfDay;
            TimeSpan? saturdayto = null;
            DateTime saturdaytodt;
            if (DateTime.TryParseExact(csvReader.GetField("SaturdayClose"), "HH:mm", null, System.Globalization.DateTimeStyles.AssumeLocal, out saturdaytodt))
                saturdayto = saturdaytodt.TimeOfDay;


            OperationHours friday = new OperationHours { day = DayOfWeek.Friday, from = fridayfrom, to = fridayto };
            OperationHours saturday = new OperationHours { day = DayOfWeek.Saturday, to = saturdayto };
            return new List<OperationHours> {  sunday, monday, tuesday, wednesday, thursday, friday, saturday } ;

        }

        private void SeedFromJson(ApplicationDbContext context)
        {
            try
            {
                dcImportContext = new ApplicationDbContext("DCImport2");
                Dictionary<string, Point3D> labeledPoints = new Dictionary<string, Point3D>();
                StringBuilder sb = new StringBuilder();
                var directory = new DirectoryInfo(JSON_DIR_PATH);
                var myFile = directory.GetFiles()
                             .OrderByDescending(f => f.LastWriteTime)
                             .First();
                string geojson = System.IO.File.ReadAllText(myFile.FullName);
                JsonTextReader reader = new JsonTextReader(new StringReader(geojson));
                reader.FloatParseHandling = FloatParseHandling.Decimal;
                JObject jObject = Newtonsoft.Json.Linq.JObject.Load(reader);
                //if (jObject.ToString().Contains("32.075524865462157"))
                //    Debugger.Break();
                List<JObject> level0 = new List<JObject>();
                List<Point3D> badPolygonsPoints = new List<Point3D>();
                List<JObject> polys = new List<JObject>();
                List<JObject> paths = new List<JObject>();
                List<Point3D> pathPoints = new List<Point3D>();
                // ramp connector points
                Dictionary<string, Point3D> pillarPoints = new Dictionary<string, Point3D>();
                List<MallBuddyApi2.Models.existing.LineStringDTO> lineStrings = new List<LineStringDTO>();
                List<JObject> connetcors = new List<JObject>();
                int noareas = 0;
                
                foreach (JObject j in jObject["features"])
                {
                    if (j != null && j["properties"]["level"] != null)
                       // | j["properties"]["level"].ToString() == "4" | j["properties"]["level"].ToString() == "5"))
                    {
                        //level0.Add(j);
                        if (j != null && j["properties"]["connector"] != null && j["properties"]["connector"].ToString() == "true")
                            connetcors.Add(j);
                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "Point"
                              && j["properties"]["attrs"] != null && j["properties"]["attrs"]["name"] != null &&
                                j["properties"]["attrs"]["name"].ToString() != String.Empty)
                        {
                            //points.Add(j);
                            Point3D result = extractPoint(noareas, j, badPolygonsPoints, new Point3D.PointType());
                            labeledPoints.Add(result.Wkt, result);
                        }
                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "Point"
&& j["properties"]["geomType"]["Type"] != null && j["properties"]["geomType"]["Type"].ToString() == "Pillar")
                        {
                            if (j["properties"]["attrs"]["name"] != null)
                            {
                                Point3D result = extractPoint(noareas, j, badPolygonsPoints, new Point3D.PointType());
                                pillarPoints.Add(result.Name,result);
                            }
                        }

                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "LineString"
      && j["properties"]["geomType"]["Type"] != null && j["properties"]["geomType"]["Type"].ToString() == "Railing")
                        {
                            extractLineString(j, lineStrings, pathPoints, labeledPoints);
                        }
                        if (j != null && j["geometry"]["type"] != null && j["geometry"]["type"].ToString() == "Polygon")
                            polys.Add(j);
                        //extractPolygon(j, labeledPoints, context);
                    }
                }
                //foreach (JObject j in points)
                //    extractPoint(noareas, j, labeledPoints);
                //removeBadPolygons(badPolygonsPoints, polys);

                //foreach (LineStringDTO ldto in lineStrings)
                //    context.LineStrings.AddOrUpdate(x => new { x.Wkt, x.Level }, ldto);
                //try
                //{
                //    context.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                //}
                //lineStrings.Clear();
                
                List<POI> poisToSave = new List<POI>();
                getMultipleLevelsConnectors(pillarPoints, lineStrings, pathPoints);
                getAdjacentLevelsConnectors(pillarPoints, lineStrings,pathPoints);
                connectPOIsToPaths(labeledPoints, lineStrings, pathPoints);
                //foreach (JObject j in paths)
                //    extractLineString(j, lineStrings);
                dcImportContext.Areas.Load();
                dcImportContext.Schedules.Load();
                dcImportContext.Categories.Load();
                
                List<POI> dcPois = dcImportContext.POIs.Include(x => x.Location).ToList();
                foreach (JObject j in polys)
                    extractPolygon(j, labeledPoints, context, badPolygonsPoints, poisToSave, dcPois);
                foreach (JObject j in connetcors)
                    extractConnector(j, context);
                foreach(LineStringDTO ldto in lineStrings)
                    context.LineStrings.AddOrUpdate(x=>new {x.Wkt,x.Level}, ldto);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                }
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
                    try
                    {
                        if (poi.Type == POI.POIType.STORE)
                        {
                            context.Stores.AddOrUpdate(new Store[] { (Store)poi });
                        }
                        else
                            context.POIs.AddOrUpdate(new POI[] { poi });



                    }
                    catch (Exception ex)
                    {
                        ex = ex;
                    }
                }
                context.SaveChanges();
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

        private void connectPOIsToPaths(Dictionary<string, Point3D> labeledPoints, List<LineStringDTO> paths, List<Point3D> pathPoints)
        {
            foreach (var item in labeledPoints.Values)
            {
                if (item.Name.ToLower().Contains("level") | item.Type == Point3D.PointType.LEVEL_CONNECTION)
                    continue;
                double minToSource = double.MaxValue;
                Point3D closestSource = null;
                foreach (Point3D pointFromAll in pathPoints)
                {
                    if (item.Equals(pointFromAll) | item.Level!=pointFromAll.Level)
                        continue;
                    double toSource = GeoUtils.GetHaversineDistance(pointFromAll, item);
                    if (toSource < minToSource)
                    {
                        minToSource = toSource;
                        closestSource = pointFromAll;
                    }
                }
                LineStringDTO pointToNearestPath = new LineStringDTO
                {
                    Level = item.Level,
                    Source = closestSource,
                    Target = item,
                    Distance = minToSource,
                    Name = "Edge To "+item.Name,
                };
                pointToNearestPath.setWktAndLocationG();
                paths.Add(pointToNearestPath);
            }

        }

        private void getMultipleLevelsConnectors(Dictionary<string, Point3D> pillarPoints, List<LineStringDTO> lineStrings, List<Point3D> pathPoints)
        {
            HashSet<string> pillarPOintsUsed = new HashSet<string>();
            List<string> pillarsList = pillarPoints.Keys.ToList();
            Dictionary<string, List<Point3D>> connectorsByLevelandType = new Dictionary<string, List<Point3D>>
            {
                {"stairsAodd", new List<Point3D>()},
                {"stairsBodd", new List<Point3D>()},
                {"stairsAeven", new List<Point3D>()},
                {"stairsBeven", new List<Point3D>()},               
                {"elevatorB", new List<Point3D>()}

            };
            //check for stairs
            foreach (string name in pillarsList)
            {
                if (name.Contains("AStairs"))
                {
                    int level = -1;
                    if (!int.TryParse(name[0].ToString(),out level))
                        continue;
                    if (level % 2 == 0)
                        connectorsByLevelandType["stairsAeven"].Add(pillarPoints[name]);
                    else
                        connectorsByLevelandType["stairsAodd"].Add(pillarPoints[name]);
                }
                if (name.Contains("BStairs"))
                {
                    int level = -1;
                    if (!int.TryParse(name[0].ToString(), out level))
                        continue;
                    if (level % 2 == 0)
                        connectorsByLevelandType["stairsBeven"].Add(pillarPoints[name]);
                    else
                        connectorsByLevelandType["stairsBodd"].Add(pillarPoints[name]);
                }
                if (name.Contains("Elevator"))
                {
                    int level = -1;
                    if (!int.TryParse(name[0].ToString(), out level))
                        continue;
                    if (level % 2 == 0)
                        connectorsByLevelandType["elevatorB"].Add(pillarPoints[name]);
                    else
                        connectorsByLevelandType["elevatorB"].Add(pillarPoints[name]);
                }
                //if (name.Contains("Esc"))
                //{
                //    int level = -1;
                //    if (!int.TryParse(name[0].ToString(), out level))
                //        continue;
                //    if (level % 2 == 0)
                //        connectorsByLevelandType["elevatorB"].Add(pillarPoints[name]);
                //    else
                //        connectorsByLevelandType["elevatorB"].Add(pillarPoints[name]);
                //}
            }
            foreach (string key in connectorsByLevelandType.Keys)
            {
                connectAllFloors(connectorsByLevelandType[key], lineStrings, pathPoints,pillarPoints);
            }
        }

        private void connectAllFloors(List<Point3D> pointsToConnect, List<LineStringDTO> lineStrings, List<Point3D> pathPoints, Dictionary<string, Point3D> pillarPoints)
        {
            for (int i = 0; i < pointsToConnect.Count; i++)
            {
                //JObject jobjectSource = pointsToConnect[i];
                Point3D source = pointsToConnect[i];
                //source = extractPoint(0, jobjectSource, null, Point3D.PointType.PATH_POINT);
                if (!pathPoints.Contains(source))
                    pathPoints.Add(source);
                else
                {
                    source = pathPoints.Find(x => x == source);
                    //source.Name = pointsToConnect[i].Name;
                }
                for (int j = 0; j < pointsToConnect.Count; j++)
                {

                    if (j == i)
                        continue;
                    LineStringDTO toAdd = new LineStringDTO();
                    //JObject jobjectTarget = pointsToConnect[j];
                    Point3D target = pointsToConnect[j];
                    //target = extractPoint(0, jobjectTarget, null, Point3D.PointType.PATH_POINT);
                    if (!pathPoints.Contains(target))
                        pathPoints.Add(target);
                    else 
                    {
                        target = pathPoints.Find(x => x == target);
                        //target.Name = pointsToConnect[j].Name;
                    }
                    toAdd.Source = source;
                    toAdd.Target = target;
                    toAdd.Wkt = "LINESTRING(" + source.Longitude + " " + source.Latitude + "," + target.Longitude + " " + target.Latitude + ")";
                    toAdd.LocationG = DbGeometry.LineFromText(toAdd.Wkt, 4326);
                    toAdd.BiDirectional = false;
                    toAdd.Distance = GeoUtils.GetHaversineDistance(source, target);
                    toAdd.Level = source.Level;
                    toAdd.setConnectorType();
                    // 
                    if (toAdd.connectorType == MallBuddyApi2.Models.existing.LineStringDTO.ConnectorType.STAIRS)
                        toAdd.Distance += (int) MallBuddyApi2.Models.existing.LineStringDTO.ConnectorTypeCost.STAIRS;
                    lineStrings.Add(toAdd);
                }
                pillarPoints.Remove(source.Name);
            }
        }

        private void getAdjacentLevelsConnectors(Dictionary<string, Point3D> pillarPoints, List<LineStringDTO> lineStrings, List<Point3D> pathPoints)
        {
            HashSet<string> pillarPOintsUsed = new HashSet<string>();
            List<string> pillarsList = pillarPoints.Keys.ToList();
            foreach (string name in pillarsList)
            {
                //if (name == "2-3BRamp")
                //    Debugger.Break();
                if (!pillarPoints.ContainsKey(name))
                    continue;
                //JObject j = pillarPoints[name];
                Point3D source = pillarPoints[name];
                //source = extractPoint(0, j, null, Point3D.PointType.PATH_POINT);
                if (source.Name.Length < 3)
                    continue;
                if (pathPoints.Contains(source))
                {
                    source = pathPoints.Find(x => x == source);
                    source.Name = name;
                }
                else
                    pathPoints.Add(source);
                char startlevel = source.Name[0];
                char endlevel = source.Name[2];
                string destname = source.Name.Remove(0,1).Insert(0,endlevel.ToString()).Remove(2,1).Insert(2,startlevel.ToString());
                if (!pillarPoints.ContainsKey(destname))
                    continue;
                LineStringDTO toAdd = new LineStringDTO();
                //j = pillarPoints[destname];
                Point3D target = pillarPoints[destname];
                //target = extractPoint(0, j, null, Point3D.PointType.PATH_POINT);
                if (pathPoints.Contains(target))
                {
                    target = pathPoints.Find(x => x == target);
                    target.Name = destname;
                }
                else
                    pathPoints.Add(target);
                target.Type = Point3D.PointType.LEVEL_CONNECTION;
                source.Type = Point3D.PointType.LEVEL_CONNECTION;
                toAdd.Source = source;
                toAdd.Target = target;
                toAdd.Wkt = "LINESTRING(" + source.Longitude + " " + source.Latitude + "," + target.Longitude + " " + target.Latitude + ")";
                toAdd.LocationG = DbGeometry.LineFromText(toAdd.Wkt,4326);
                toAdd.Distance = GeoUtils.GetHaversineDistance(source, target);
                toAdd.Level = source.Level;
                toAdd.BiDirectional = true;
                if (source.Name.Contains("Esc"))
                    if (!source.Name.Contains("EscBi"))
                        toAdd.BiDirectional = false;
                toAdd.setConnectorType();
                lineStrings.Add(toAdd);
                pillarPoints.Remove(name);
                pillarPoints.Remove(destname);
            }
        }


        private void extractLineString(JObject feature, List<LineStringDTO> lineStrings, List<Point3D> pathPoints, Dictionary<string,Point3D> labeledPoints)
        {
            LineStringDTO toAdd = new LineStringDTO();
            int level = int.Parse(feature["properties"]["level"].ToString());
            //bool isAccessible = Boolean.Parse(feature["properties"]["accessible"].ToString());
            toAdd.Level = level;
            if (feature["properties"]["attrs"]["name"]!=null)
                toAdd.Name = feature["properties"]["attrs"]["name"].ToString();
            StringBuilder wktsb = new StringBuilder("LINESTRING (");
            //List<Point3D> labeledPointsFound = new List<Point3D>();
            //Point3D source = new Point3D();
            //Point3D target = new Point3D();
            int i = 0;
            foreach (JArray j in feature["geometry"]["coordinates"])
            {
                StringBuilder pointWkt = new StringBuilder("POINT (");
                string lon = Regex.Match(j.ToString(), "34\\.\\d+").Value;
                string lat = Regex.Match(j.ToString(), "32\\.\\d+").Value;
                wktsb.Append(lon + " " + lat + ",");
                pointWkt.Append(lon + " " + lat + ")");
                Point3D current = null;
                if(labeledPoints.ContainsKey(pointWkt.ToString()))
                {
                    current = labeledPoints[pointWkt.ToString()];
                    current.Type = Point3D.PointType.PATH_POINT;
                    if (!pathPoints.Contains(current))
                        pathPoints.Add(current);
                }
                if (current == null)
                    current = pathPoints.Find(x => x.Wkt == pointWkt.ToString() && x.Level == level);
                else
                    current = current;

                if (current == null)
                {
                    current = new Point3D();
                    //current = i == 0 ? source : target;
                    current.Latitude = Decimal.Parse(lat);
                    current.Longitude = Decimal.Parse(lon);
                    current.Wkt = pointWkt.ToString();
                    current.LocationG = DbGeometry.PointFromText(current.Wkt, 4326);
                    current.Level = level;
                    current.Type = Point3D.PointType.PATH_POINT;
                    pathPoints.Add(current);
                }
                else
                {
                    i = i;
                }
                if (i == 0)
                    toAdd.Source = current;
                if (i == 1)
                    toAdd.Target = current;
                i++;
            }
            wktsb.Remove(wktsb.Length - 1, 1);
            wktsb.Append(")");
            toAdd.Wkt = wktsb.ToString();
            toAdd.LocationG = DbGeometry.LineFromText(toAdd.Wkt, 4326);
            toAdd.Distance = GeoUtils.GetHaversineDistance(toAdd.Source, toAdd.Target);
            toAdd.BiDirectional = true;
            //if (toAdd.Target.Longitude == 34.7745353108864M)
             //   Debugger.Break();
            toAdd.setConnectorType();
            lineStrings.Add(toAdd);
        }

        private void extractConnector(JObject j, ApplicationDbContext context)
        {
            //throw new NotImplementedException();
        }

        private void extractPolygon(JObject feature, Dictionary<string, Point3D> labeledPoints, ApplicationDbContext context
            , List<Point3D> badPolygonsPoints, List<POI> poisToSave, List<POI> dcPois)
        {
            POI poi = new POI();
            poi.Type = null;
            poi.Modified = DateTime.Now;
            Polygone toReturn = new Polygone();
            toReturn.Points = new List<Point3D>();
            int level = int.Parse(feature["properties"]["level"].ToString());
            //bool isAccessible = Boolean.Parse(feature["properties"]["accessible"].ToString());
            toReturn.Level = level;
            //if (level == 6)
           //    Debugger.Break();
            StringBuilder wktsb = new StringBuilder("POLYGON ((");
            List<Point3D> labeledPointsFound = new List<Point3D>();
            foreach (JArray j in feature["geometry"]["coordinates"][0])
            {
                StringBuilder pointWkt = new StringBuilder("POINT (");
                string lon = Regex.Match(j.ToString(), "34\\.\\d+").Value;
                string lat = Regex.Match(j.ToString(), "32\\.\\d+").Value;
                wktsb.Append(lon + " " + lat + ",");
                pointWkt.Append(lon + " " + lat + ")");
                //if (pointWkt.ToString() == "POINT (34.776239992664784 32.07554239891567)")
                  //  Debugger.Break();
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

                //foreach (Point3D point in badPolygonsPoints)
                   // if (polygone.Contains(point.LocationG))
                       // return;
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
            Point3D labeledPoint = null;
            if ((labeledPoint = labeledPointsFound.FirstOrDefault(x => x.Name.ToLower().Contains("level"))) == null)
                labeledPoint = labeledPointsFound[0];
            else
                labeledPoint = labeledPoint;
            POI fromDCInfo = null;
            if (labeledPoint != null)
            {
                if (labeledPoint.Areas.Count > 0)
                {
                    try
                    {
                        fromDCInfo = dcPois.Find(x =>x.Location!=null && x.Location.Areas!=null && x.Location.Areas.Contains(labeledPoint.Areas[0]));
                    }
                    catch(Exception ex)
                    {
                        ex = ex;
                    }
                    //if (labeledPoint.Name != null && labeledPoint.Name.Contains("נייק"))
                    //    Debugger.Break();
                    if (fromDCInfo != null)
                    {
                        //if (fromDCInfo is ISchedulable)
                        //    dcImportContext.Entry(fromDCInfo).Collection(x => ((ISchedulable)x).Schedule).Load();
                        toReturn.Areas = fromDCInfo.Location.Areas;
                        fromDCInfo.Location = toReturn;
                        toReturn.POI = fromDCInfo;
                        if (fromDCInfo is IHasEntrances)
                        {
                            ((IHasEntrances)fromDCInfo).Entrances = new List<Point3D>();
                            foreach (Point3D entrancePoint in labeledPointsFound)
                            {
                                ((IHasEntrances)fromDCInfo).Entrances.Add(entrancePoint);
                                entrancePoint.Type = Point3D.PointType.ENTRANCE;
                            }
                        }
                        //toReturn.Areas = fromDCInfo.Location.Areas;
                        fromDCInfo.Level = level;
                        fromDCInfo.Modified = DateTime.Now;
                        poisToSave.Add(fromDCInfo);
                        return;
                    }

                }
                if (labeledPoint.Areas != null && labeledPoint.Areas.Count > 0 && !labeledPoint.Areas[0].AreaID.StartsWith("9"))
                    poi.Type = POI.POIType.STORE;
                if (labeledPoint.Name.ToLower().Contains("atm"))
                    poi.Type = POI.POIType.ATM;
                if (labeledPoint.Name.ToLower().Contains("entrance"))
                    poi.Type = POI.POIType.ENTRANCE;
                if (labeledPoint.Name.ToLower().Contains("toilet") | labeledPoint.Name.ToLower().Contains("wc"))
                    poi.Type = POI.POIType.WC;
                if (labeledPoint.Name.ToLower().Contains("deadzone"))
                    poi.Type = POI.POIType.DEADZONE;
                if (labeledPoint.Name.ToLower().Contains("level"))
                    poi.Type = POI.POIType.HOSTED_LEVEL;
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
                                Enabled = true,
                                IsAccessible = labeledPoint.IsAccessible,
                                Name = labeledPoint.Name,
                                Name2 = labeledPoint.Name2,
                                Location = toReturn,
                                Entrances = new List<Point3D>()
                            };
                            foreach (Point3D entrancePoint in labeledPointsFound)
                            {
                                ((Store)poi).Entrances.Add(entrancePoint);
                                entrancePoint.Type = Point3D.PointType.ENTRANCE;
                            }
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
                poi.Level = level;
                poi.Modified = DateTime.Now;
                poisToSave.Add(poi);
            }
            //return poi;
        }

        private static Point3D extractPoint(int noareas, JObject feature, List<Point3D> badPolygonsPoints, Point3D.PointType pointType)
        {
            Point3D toReturn = new Point3D();
            toReturn.Level = int.Parse(feature["properties"]["level"].ToString());
            //if (toReturn.Level == 6)
             //   Debugger.Break();
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
            //if (toReturn.Name.ToLower().Contains("level"))
              //  Debugger.Break();
            toReturn.Latitude = Decimal.Parse(lat);
            toReturn.Longitude = Decimal.Parse(lon);
            if (feature["properties"]["attrs"]["Name2"] != null)
                toReturn.Name2 = feature["properties"]["attrs"]["Name2"].ToString();
            if (pointType != Point3D.PointType.UNDEFINED)
                toReturn.Type = pointType;
            //if (toReturn.Name != null && toReturn.Name.Contains("כלי זמר"))
            //    Debugger.Break();
            toReturn.Areas = new List<Area>();
            if ((feature["properties"]["attrs"]["AreaID"] != null && !feature["properties"]["attrs"]["AreaID"].ToString().Equals(""))
              | (feature["properties"]["attrs"]["AREAID"] != null && !feature["properties"]["attrs"]["AREAID"].ToString().Equals("")))
            {
                if (feature["properties"]["attrs"]["AreaID"] != null && !feature["properties"]["attrs"]["AreaID"].ToString().Equals(""))
                {
                    Area area = new Area(feature["properties"]["attrs"]["AreaID"].ToString());
                    toReturn.Areas.Add(area);
                }
                if (feature["properties"]["attrs"]["AREAID"] != null && !feature["properties"]["attrs"]["AREAID"].ToString().Equals(""))
                {
                    Area area = new Area(feature["properties"]["attrs"]["AREAID"].ToString());
                    toReturn.Areas.Add(area);
                }
                int index = 2;
                while (true)
                {
                    bool found = false;
                    if (feature["properties"]["attrs"]["AREAID" + index] != null && feature["properties"]["attrs"]["AREAID" + index].ToString() != "")
                    {
                        found = true;
                        toReturn.Areas.Add(new Area(feature["properties"]["attrs"]["AREAID" + index].ToString()));
                    }
                    if (feature["properties"]["attrs"]["AreaID" + index] != null && feature["properties"]["attrs"]["AreaID" + index].ToString() != "")
                    {
                        found = true;
                        toReturn.Areas.Add(new Area(feature["properties"]["attrs"]["AreaID" + index].ToString()));
                    }
                    if (!found)
                        break;
                    index++;
                }
            }
            else
            {
                noareas++;
            }
            if (badPolygonsPoints!=null && (toReturn.Name.ToUpper().Contains("PASSAGES") | toReturn.Name.ToUpper().Contains("ROADWALK")))
                badPolygonsPoints.Add(toReturn);
            return toReturn;

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
