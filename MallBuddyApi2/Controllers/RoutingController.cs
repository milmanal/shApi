using MallBuddyApi2.Models;
using MallBuddyApi2.Models.existing;
using MallBuddyApi2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MallBuddyApi2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoutingController : ApiController
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET api/routing
        public RoutingPath GetRoute(string lon1, string lat1, int level1, string lon2, string lat2, int level2)
        {

            Point3D sourceToFind = new Point3D { Latitude = Decimal.Parse(lat1), Type = Point3D.PointType.PATH_POINT, Level = level1, Longitude = Decimal.Parse(lon1) };
            Point3D targetToFind = new Point3D { Latitude = Decimal.Parse(lat2), Type = Point3D.PointType.PATH_POINT, Level = level2, Longitude = Decimal.Parse(lon2) };
            Point3D source = db.Points.Where(x=> x.Level == level1&&x.Latitude == sourceToFind.Latitude && x.Longitude == sourceToFind.Longitude).FirstOrDefault();
            Point3D target = db.Points.Where(x => x.Level == level2 && x.Latitude == targetToFind.Latitude && x.Longitude == targetToFind.Longitude).FirstOrDefault();
            if (source == null)
            {
                source = sourceToFind;
                source.setWktAndGeometry();
            } 
            if (target == null)
            {
                target = targetToFind;
                target.setWktAndGeometry();
            }
            var pathPoints = db.Points.Where(x => x.Type == Point3D.PointType.PATH_POINT);
            double minToSource = double.MaxValue;
            double minToTarget = double.MaxValue;
            Point3D closestTarget = null;
            Point3D closestSource = null;
            foreach (Point3D point in pathPoints)
            {
                double toSource = GeoUtils.GetHaversineDistance(point, source);
                if (toSource < minToSource)
                {
                    minToSource = toSource;
                    closestSource = point;
                }
                double toTarget = GeoUtils.GetHaversineDistance(point, target);
                if (toTarget < minToTarget)
                {
                    minToTarget = toTarget;
                    closestTarget = point;
                }
            }
            LineString3D<Point3D> sourceToNearestPath = new LineString3D<Point3D>
            {
                Level = source.Level,
                Source = source,
                Target = closestSource,
                Distance = minToSource
            };
            sourceToNearestPath.setWktAndLocationG();
            LineString3D<Point3D> targetToNearestPath = new LineString3D<Point3D>
            {
                Level = target.Level,
                Source = target,
                Target = closestTarget,
                Distance = minToTarget
            };
            targetToNearestPath.setWktAndLocationG();
            List<LineString3D<Point3D>> edges = new List<LineString3D<Point3D>>();
            foreach (LineStringDTO item in db.LineStrings)
                edges.Add(item.toGeneric());
            edges.Add(sourceToNearestPath);
            edges.Add(targetToNearestPath);
            Graph graph = new Graph(edges);
            RoutingPath route = graph.GetDijkstraPath(source, target);
            //Point3D source = new Point3D { Latitude = Decimal.Parse(lat1), Longitude = Decimal.Parse(lon1), Level = level1 };
            //Point3D dest = new Point3D { Latitude = Decimal.Parse(lat2), Longitude = Decimal.Parse(lon2), Level = level2 };

            return route;
        }

    }
}
