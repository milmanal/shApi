﻿using MallBuddyApi2.Models;
using MallBuddyApi2.Models.existing;
using MallBuddyApi2.Models.existing.Routing;
using MallBuddyApi2.Models.existing.Routing.Graph21;
using MallBuddyApi2.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MallBuddyApi2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoutingController : ApiController
    {
        PoisRepository poisRep = new PoisRepository();
        //private ApplicationDbContext db = new ApplicationDbContext();
        // GET api/routing
        public RoutingPath GetRoute(string lon1, string lat1, int level1, string lon2, string lat2, int level2)
        {
            using (ApplicationDbContext myContext = new ApplicationDbContext())
            {
                RoutingPath totalPath = GetRoutingPath(myContext, lon1, lat1, level1, lon2, lat2, level2);
                //Graph graph = new Graph(edges);
                //RoutingPath route = graph.GetDijkstraPath(source, target);
                //Point3D source = new Point3D { Latitude = Decimal.Parse(lat1), Longitude = Decimal.Parse(lon1), Level = level1 };
                //Point3D dest = new Point3D { Latitude = Decimal.Parse(lat2), Longitude = Decimal.Parse(lon2), Level = level2 };
                //RoutingPath allnet = new RoutingPath{Routingsteps = new List<RoutingStep>()};
                // foreach (var step in db.LineStrings)
                //     allnet.Routingsteps.Add(new RoutingStep { Source = step.Source, Destination = step.Target, Distance = step.Distance });
                //string geojson = allnet.ToGeojson();
                //string geojson = totalPath.ToGeojson();
                //StringContent sc = new StringContent(geojson);
                //sc.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //HttpResponseMessage resp = new HttpResponseMessage();
                //resp.Content = sc;

                //return resp;
                return totalPath;
            }
        }

        private List<GraphDataItem> GetAdditionalEdgeOfSourceOrTargetToNearest(ApplicationDbContext db, List<IHasEntrances> storeContainers, Point3D point, IQueryable<Point3D> pathPoints, bool isTarget)
        {
            List<GraphDataItem> toReturn = new List<GraphDataItem>();
            if (storeContainers.Count == 1)
            {
                int maxid = (int)db.LineStrings.Max(x => x.Id);
                if (storeContainers[0].Entrances == null)
                {
                    db.Stores.Attach((Store)storeContainers[0]);
                    db.Entry(storeContainers[0]).Collection(x => x.Entrances).Load();
                }
                foreach (var item in storeContainers[0].Entrances)
                {
                    double distance = GeoUtils.GetHaversineDistance(point, item);
                    toReturn.Add(new GraphDataItem
                    {
                        Cost = distance,
                        EdgeID = maxid++,
                        IsReverse = true,
                        ReverseCost = distance,
                        SourceVertexID = isTarget ? (int)item.Id : (int)point.Id,
                        TargetVertexID = isTarget ? (int)point.Id : (int)item.Id,
                        Name = item.Name,
                    });
                }
            }
            if (storeContainers.Count == 0)
            {
                double minToSource = double.MaxValue;
                Point3D closestSource = null;
                foreach (Point3D pointFromAll in pathPoints.Where(x=>x.Level == point.Level))
                {
                    double toSource = GeoUtils.GetHaversineDistance(pointFromAll, point);
                    if (toSource < minToSource)
                    {
                        minToSource = toSource;
                        closestSource = pointFromAll;
                    }
                }
                if (closestSource == null)
                    return null;
                LineString3D<Point3D> pointToNearestPath = new LineString3D<Point3D>
                {
                    Level = point.Level,
                    Source = isTarget ? closestSource : point,
                    Target = isTarget ? point : closestSource,
                    Distance = minToSource,
                    Name = "Edge To Target"
                };

                if (isTarget)
                    pointToNearestPath.Name = "Edge from source";
                pointToNearestPath.setWktAndLocationG();
                toReturn.Add(new GraphDataItem { Cost = pointToNearestPath.Distance, ReverseCost = pointToNearestPath.Distance, EdgeID = (int)pointToNearestPath.Id, IsReverse = true, SourceVertexID = (int)pointToNearestPath.Source.Id, TargetVertexID = (int)pointToNearestPath.Target.Id });
            }
            return toReturn;
        }

        private RoutingPath GetRoutingPath(ApplicationDbContext myContext, string lon1, string lat1, int level1, string lon2, string lat2, int level2)
        {
            List<GraphDataItem> dtEdges = new List<GraphDataItem>();
            RoutingPath totalPath = null;

            Point3D sourceToFind = new Point3D { Latitude = Decimal.Parse(lat1), Type = Point3D.PointType.PATH_POINT, Level = level1, Longitude = Decimal.Parse(lon1) };
            Point3D targetToFind = new Point3D { Latitude = Decimal.Parse(lat2), Type = Point3D.PointType.PATH_POINT, Level = level2, Longitude = Decimal.Parse(lon2) };
            Point3D source = myContext.Points.ToList().Where(x => x.Level == level1 && x.Latitude == sourceToFind.Latitude && x.Longitude == sourceToFind.Longitude).FirstOrDefault();
            Point3D target = myContext.Points.ToList().Where(x => x.Level == level2 && x.Latitude == targetToFind.Latitude && x.Longitude == targetToFind.Longitude).FirstOrDefault();
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
            var pathPoints = myContext.Points.Where(x => x.Type == Point3D.PointType.PATH_POINT | x.Type == Point3D.PointType.LEVEL_CONNECTION);
            //double minToTarget = double.MaxValue;
            //Point3D closestTarget = null;
            foreach (LineStringDTO item in myContext.LineStrings.Include("Source").Include("Target").ToList())
            {
                //if (item.connectorType != ConnectorType.NONE)
                //  System.Diagnostics.Debugger.Break();
                dtEdges.Add(new GraphDataItem
                {
                    Name = item.Name,
                    Cost = item.Distance,
                    EdgeID = (int)item.Id,
                    IsReverse = item.BiDirectional,
                    ReverseCost = item.BiDirectional ? item.Distance : double.PositiveInfinity,
                    SourceVertexID = (int)item.Source.Id,
                    TargetVertexID = (int)item.Target.Id
                });
                //edges.Add(item.toGeneric());
            }
            List<IHasEntrances> sourceContainers = new List<IHasEntrances>();
            List<IHasEntrances> targetContainers = new List<IHasEntrances>();

            //GetStoreContainersOfPoint(myContext, source, target, sourceContainers, targetContainers);

            List<POI> containersSource = poisRep.GetContainerByLocation(lon1, lat1, level1);
            foreach (var item in containersSource)
            {
                if (item is IHasEntrances)
                    sourceContainers.Add((IHasEntrances)item);
            }
            List<POI> containersTarget = poisRep.GetContainerByLocation(lon2, lat2, level2);
            foreach (var item in containersTarget)
            {
                if (item is IHasEntrances)
                    targetContainers.Add((IHasEntrances)item);
            }
            //point is inside store - need to add the edge to the entrance first
            List<GraphDataItem> edgesFromSourceToGraph = GetAdditionalEdgeOfSourceOrTargetToNearest(myContext, sourceContainers, source, pathPoints, false);
            if (edgesFromSourceToGraph == null)
                return getRoutingPathFromDataGraphItems(myContext, source, target, dtEdges, true);
            else
                dtEdges.AddRange(edgesFromSourceToGraph);

            List<GraphDataItem> edgesFromGraphToTarget = GetAdditionalEdgeOfSourceOrTargetToNearest(myContext, targetContainers, target, pathPoints, true);
            if (edgesFromGraphToTarget == null)
                return getRoutingPathFromDataGraphItems(myContext, source, target, dtEdges, true);
            else
                dtEdges.AddRange(edgesFromGraphToTarget);
            //dtEdges.AddRange(GetAdditionalEdgeOfSourceOrTargetToNearest(myContext, targetContainers, target, pathPoints, true));

            //LineString3D<Point3D> targetToNearestPath = new LineString3D<Point3D>
            //{
            //    Level = target.Level,
            //    Source = target,
            //    Target = closestTarget,
            //    Distance = minToTarget
            //};
            //targetToNearestPath.setWktAndLocationG();
            ////List<Point3D> fromandTo = new List<Point3D> { source, target };
            ////pathPoints.Concat(fromandTo);
            ////List<LineString3D<Point3D>> edges = new List<LineString3D<Point3D>>();

            //dtEdges.Add(new GraphDataItem { Cost = targetToNearestPath.Distance, EdgeID = (int)targetToNearestPath.Id, IsReverse = false, SourceVertexID = (int)targetToNearestPath.Source.Id, TargetVertexID = (int)targetToNearestPath.Target.Id });
            //edges.Add(sourceToNearestPath);
            //edges.Add(targetToNearestPath);
            Graph2 graph2 = new Graph2(dtEdges);
            GraphSearchResult result = graph2.GetShortestPath((int)source.Id, (int)target.Id);
            if (result == null || result.Count() == 0)
            {
                //totalPath = getRoutingPathFromDataGraphItems(myContext, source, target, dtEdges, true);
            }
            else
                totalPath = getRoutingPathFromDataGraphItems(myContext, source, target, result, false);

            return totalPath;
        }

        private RoutingPath getRoutingPathFromDataGraphItems(ApplicationDbContext db, Point3D source, Point3D target, IEnumerable<GraphDataItem> result, bool isAllEdges)
        {
            RoutingPath toReturn = new RoutingPath { Routingsteps = new List<RoutingStep>(), Source = source, Destination = target };
            foreach (var step in result)
            {
                Point3D from = null;
                LineStringDTO ls = db.LineStrings.ToList().Where(x=>x.Id == step.EdgeID).FirstOrDefault();

                RoutingStep next = new RoutingStep();
                if (ls != null)
                {
                   // if (ls.connectorType != ConnectorType.PATH)
                     //   System.Diagnostics.Debugger.Break();
                    next = ls.toGeneric().toRoutingStep();
                }
                if (step.SourceVertexID == source.Id | step.SourceVertexID == target.Id)
                {
                    //step.EdgeID == sourceToNearestPath.Id ? lineDTO = sourceToNearestPath.toDTO() : lineDTO = targetToNearestPath.toDTO();
                    from = step.SourceVertexID == source.Id ? source : target;
                }
                else
                    from = db.Points.Find(step.SourceVertexID);
                Point3D to = null;
                if (step.TargetVertexID == source.Id | step.TargetVertexID == target.Id)
                {
                    //step.EdgeID == sourceToNearestPath.Id ? lineDTO = sourceToNearestPath.toDTO() : lineDTO = targetToNearestPath.toDTO();
                    to = step.TargetVertexID == source.Id ? source : target;
                }
                else
                    to = db.Points.Find(step.TargetVertexID);
                //Point3D to = db.Points.Find(step.TargetVertexID);
                //Point3D from = pathPoints.Where(x => x.Id == step.TargetVertexID).FirstOrDefault();
                //Point3D to = pathPoints.Where(x=>x.Id == step.TargetVertexID).FirstOrDefault();
                if (from == null | to == null)
                    System.Diagnostics.Debugger.Break();
                //LineStringDTO lineDTO = null;
                //if (step.EdgeID == sourceToNearestPath.Id | step.EdgeID == targetToNearestPath.Id)
                //{
                //    //step.EdgeID == sourceToNearestPath.Id ? lineDTO = sourceToNearestPath.toDTO() : lineDTO = targetToNearestPath.toDTO();
                //    lineDTO = step.EdgeID == sourceToNearestPath.Id ? sourceToNearestPath.toDTO() : targetToNearestPath.toDTO();
                //}
                //else
                //    lineDTO = db.LineStrings.Find(step.EdgeID);
                //totalPath2.Routingsteps.Add(lineDTO.toGeneric().toRoutingStep());
                //RoutingStep next = null;
                if (toReturn.Routingsteps.Count == 0)
                {
                    if (step.SourceVertexID == source.Id)
                        next.setSourceTargetDistance(from, to, step.Cost );
                    else if (step.TargetVertexID == source.Id)
                        next.setSourceTargetDistance(to, from, step.Cost);
                    else
                        if (isAllEdges)
                            next.setSourceTargetDistance(from, to, step.Cost);
                }
                else
                {
                    if (step.SourceVertexID == toReturn.Routingsteps[toReturn.Routingsteps.Count - 1].Destination.Id)
                        next.setSourceTargetDistance(from, to, step.Cost);
                    else if (step.TargetVertexID == toReturn.Routingsteps[toReturn.Routingsteps.Count - 1].Destination.Id)
                        next.setSourceTargetDistance(to, from, step.Cost);
                    else
                        if (isAllEdges)
                            next.setSourceTargetDistance(from, to, step.Cost);
 
                }
                toReturn.Routingsteps.Add(next);
                toReturn.Distance += step.Cost;
            }
            return toReturn;
        }

        private void GetStoreContainersOfPoint(ApplicationDbContext db, Point3D source, Point3D target, List<IHasEntrances> sourceContainers, List<IHasEntrances> targetContainers)
        {
            var levelPois = db.POIs.Include("Location").Where(x => x.Location.Level == source.Level | x.Location.Level == target.Level);
            //var levelPois = BuildSet<IHasEntrances>().Where(x => ((POI)x).Location.Level == source.Level | ((POI)x).Location.Level == target.Level);
            
            DbGeometry sourcePoint = DbGeometry.PointFromText("POINT (" + source.Longitude + " " + source.Latitude + ")", 4326);
            DbGeometry targetPoint = DbGeometry.PointFromText("POINT (" + target.Longitude + " " + target.Latitude + ")", 4326);

            List<POI> containers = new List<POI>();
            foreach (var s in levelPois)
            {
                
                if (s is IHasEntrances)
                {
                    //db.Entry(s).Reference("Location").Load();
                    if (((POI)s).Location.LocationG.Contains(sourcePoint))
                        sourceContainers.Add((IHasEntrances)(s));
                    if (((POI)s).Location.LocationG.Contains(targetPoint))
                        targetContainers.Add((IHasEntrances)(s));
                }

                //if (s is IHasEntrances && ((POI)s).Location.LocationG.Contains(targetPoint))
                //    targetContainers.Add((IHasEntrances)(s));

                //if (s is IHasEntrances && GeoUtils.IsPointInPolygone(source, s.Location))
                //    sourceContainers.Add((IHasEntrances)(s));
                //if (s is IHasEntrances && GeoUtils.IsPointInPolygone(target, s.Location))
                //    targetContainers.Add((IHasEntrances)(s));
            }
            
        }

        //public List<IHasEntrances> BuildSet<T>() where T : class
        //{
        //    return db.Set<T>().Include("Location").Include("Entrance").ToList().Cast<IHasEntrances>().ToList();
        //}

        [Route("api/routing/geojson")]
        public HttpResponseMessage GetRouteGeoJson(string lon1, string lat1, int level1, string lon2, string lat2, int level2)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                RoutingPath totalPath = GetRoutingPath(context,lon1, lat1, level1, lon2, lat2, level2);

                //string geojson = allnet.ToGeojson();
                string geojsonString = totalPath.ToGeojson();
                StringContent sc = new StringContent(geojsonString);
                sc.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage resp = new HttpResponseMessage();
                resp.Content = sc;

                return resp;
            }
        }

        [Route("api/routing/byfloor")]
        public RoutingPathByFloor GetRouteByFloor(string lon1, string lat1, int level1, string lon2, string lat2, int level2)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                RoutingPath totalPath = GetRoutingPath(context, lon1, lat1, level1, lon2, lat2, level2);
                Dictionary<int, List<POI>> ada = new Dictionary<int, List<POI>>();
                int startlevel = Math.Min(level1, level2);
                int endlevel = Math.Max(level1, level2);
                var results = context.POIs.Include("Location").Where(x => x.Type == POI.POIType.HOSTED_LEVEL & x.Location.Level >= startlevel & x.Location.Level <= endlevel);
                foreach (var item in results)
                {
                    if (!ada.ContainsKey(item.Location.Level))
                        ada[item.Location.Level] = new List<POI>();
                    ada[item.Location.Level].Add(item);
                }

                //ada = context.POIs.Include("Location").Where(x=>x.Type == POI.POIType.HOSTED_LEVEL & x.Location.Level>=startlevel& x.Location.Level<=endlevel).GroupBy(x => x.Location.Level).ToDictionary(x=>x.Key, x=>x.ToList());
                RoutingPathByFloor routingPathByFloor = totalPath.ToRoutingPathByFloor(ada);
                return routingPathByFloor;
            }
        }
    }
}
