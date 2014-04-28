using MallBuddyApi2.Models.existing.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class RoutingPath
    {
        public Point3D Source { get; set; }
        public Point3D Destination { get; set; }
        public List<RoutingStep> Routingsteps { get; set; }
        public double Distance { get; set; }
        private string geoJson { get; set; }
        [JsonIgnore]
        public string GeoJson
        {
            get
            {
                if (string.IsNullOrEmpty(geoJson))
                    geoJson = ToGeojson();
                return geoJson;
            }
            set
            {
                geoJson = value;
            }
        }
        public String ToGeojson()
        {
            StringBuilder sb = new StringBuilder(@"{""type"":""FeatureCollection"",""features"":[");
            foreach (RoutingStep rstep in Routingsteps)
                sb.Append(rstep.ToGeojson()+",");
            sb.Remove(sb.Length-1,1);
            sb.Append("]}");
            return sb.ToString();
        }

        public RoutingPathByFloor ToRoutingPathByFloor (Dictionary<int, List<POI>> hostedLevels)
        {
            List<RoutingPoint> pointsList = new List<RoutingPoint>();
            RoutingPathByFloor toReturn = new RoutingPathByFloor 
            { 
                Source = this.Source, 
                Destination = this.Destination, 
                Distance = this.Distance, 
                PointsOfFloors = new Dictionary<int, List<RoutingPoint>>() 
            };
            //List<int> levelsByOrder = Routingsteps.Select(x => x.Source.Level).Concat(new List<int> { Destination.Level }).Distinct().ToList();
            double coveredDistance = 0;
            // treat the first point separately
            if (!toReturn.PointsOfFloors.ContainsKey(Source.Level))
                toReturn.PointsOfFloors[Source.Level] = new List<RoutingPoint>();
            int floorsDirectionDelta = Routingsteps[0].Source.Level >= Routingsteps[0].Destination.Level ? -1 : 1;

            RoutingPoint firstpoint = Routingsteps[0].toRoutingPoint(coveredDistance, true);
            firstpoint.DistanceCovered = 0;
            toReturn.PointsOfFloors[Source.Level].Add(firstpoint);
            pointsList.Add(firstpoint);
            coveredDistance = firstpoint.DistanceCovered;
            if (hostedLevels.ContainsKey(Source.Level + floorsDirectionDelta) && hostedLevels[Source.Level + floorsDirectionDelta] != null)
                foreach (POI poi in hostedLevels[Source.Level + floorsDirectionDelta])
                {
                    int levelIndex = int.Parse(poi.Name[5].ToString());
                    if (levelIndex == Source.Level && poi.Location.LocationG.Contains(Source.LocationG))
                    {
                        if (!toReturn.PointsOfFloors.ContainsKey(Source.Level + floorsDirectionDelta))
                            toReturn.PointsOfFloors[Source.Level + floorsDirectionDelta] = new List<RoutingPoint>();
                        toReturn.PointsOfFloors[Source.Level + floorsDirectionDelta].Add(new RoutingPoint(firstpoint));
                    }
                }
            RoutingPoint point = null;
            List<int> previousLevels = new List<int>();
            List<int> currentLevels = new List<int>();
            for (int i=0; i<Routingsteps.Count; i++)
            {
                RoutingStep step = Routingsteps[i];
                int levelToAdd =  step.Source.Level;
                if (!string.IsNullOrEmpty(step.Instructions))
                {
                    levelToAdd = step.Destination.Level;
                    point.Instructions = step.Instructions;

                    // set instruction in few steps before, in 20m distance
                    
                    for (int j=pointsList.Count-1; j>0; j--)
                    {
                        if ((coveredDistance) - pointsList[j].DistanceCovered < 20)
                            pointsList[j].Instructions = step.Instructions;
                        else
                            break;
                    }
                }
                if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd))
                    toReturn.PointsOfFloors[levelToAdd] = new List<RoutingPoint>();
                point = step.toRoutingPoint(coveredDistance, false);
                //if (step == Routingsteps[0])
                  //  continue;
                toReturn.PointsOfFloors[levelToAdd].Add(point);
                pointsList.Add(point);
                currentLevels.Add(levelToAdd);
                coveredDistance = point.DistanceCovered;
                if (this.Distance - coveredDistance < 20 && Math.Abs(levelToAdd - Destination.Level) <= 1)
                    point.Instructions = "היכון להגעה ליעד";
                floorsDirectionDelta = step.Source.Level >= step.Destination.Level ? -1 : 1;
                if (hostedLevels.ContainsKey(levelToAdd + floorsDirectionDelta) && hostedLevels[levelToAdd + floorsDirectionDelta] != null)
                    foreach (POI poi in hostedLevels[levelToAdd + floorsDirectionDelta])
                    {
                        int levelIndex = int.Parse(poi.Name[5].ToString());
                        //if (levelIndex == levelToAdd && MallBuddyApi2.Utils.GeoUtils.IsPointInPolygone(step.Destination,poi.Location))
                        if (levelIndex == levelToAdd && poi.Location.LocationG.Contains(point.LocationG))
                        {
                            if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd + floorsDirectionDelta))
                                toReturn.PointsOfFloors[levelToAdd + floorsDirectionDelta] = new List<RoutingPoint>();
                            toReturn.PointsOfFloors[levelToAdd + floorsDirectionDelta].Add(new RoutingPoint(point));
                            currentLevels.Add(levelToAdd + floorsDirectionDelta);
                            poi.Location.LocationG.AsText();
                        }
                    }

                if (hostedLevels.ContainsKey(levelToAdd - floorsDirectionDelta) && hostedLevels[levelToAdd - floorsDirectionDelta] != null)
                    foreach (POI poi in hostedLevels[levelToAdd - floorsDirectionDelta])
                    {
                        int levelIndex = int.Parse(poi.Name[5].ToString());
                        if (levelIndex == levelToAdd && poi.Location.LocationG.Contains(point.LocationG))
                        {
                            if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd - floorsDirectionDelta))
                                toReturn.PointsOfFloors[levelToAdd - floorsDirectionDelta] = new List<RoutingPoint>();
                            toReturn.PointsOfFloors[levelToAdd - floorsDirectionDelta].Add(new RoutingPoint(point));
                            currentLevels.Add(levelToAdd - floorsDirectionDelta);
                        }
                    }
                // check if a null needs to be inserted in the array of one of the levels - means we recognzied that a segment has ended within a floor
                // A NULL may stand for the end of the segment which may have (or not) another segment within the floor after
                foreach (var levelPreviouslyAdded in previousLevels)
                {
                    if (!currentLevels.Contains(levelPreviouslyAdded))
                    {
                        toReturn.PointsOfFloors[levelPreviouslyAdded][toReturn.PointsOfFloors[levelPreviouslyAdded].Count - 1].SkipConnectToNext = true;
                        toReturn.PointsOfFloors[levelPreviouslyAdded].Add(null);
                    }
                }
                previousLevels = currentLevels.ToList();
                currentLevels.Clear();
            }
            // treat the last point separately
            //if (!toReturn.PointsOfFloors.ContainsKey(Destination.Level))
            //    toReturn.PointsOfFloors[Destination.Level] = new List<RoutingPoint>();
            //RoutingPoint lastpoint = Routingsteps[Routingsteps.Count-1].toRoutingPoint(coveredDistance, false);
            //toReturn.PointsOfFloors[Destination.Level].Add(lastpoint);
            ////coveredDistance = lastpoint.DistanceCovered;
            //if (hostedLevels.ContainsKey(Destination.Level + floorsDirectionDelta) && hostedLevels[Destination.Level + floorsDirectionDelta] != null)
            //    foreach (POI poi in hostedLevels[Destination.Level + floorsDirectionDelta])
            //    {
            //        int levelIndex = int.Parse(poi.Name[5].ToString());
            //        if (levelIndex == Destination.Level + floorsDirectionDelta && poi.Location.LocationG.Contains(Destination.LocationG))
            //            toReturn.PointsOfFloors[Destination.Level + floorsDirectionDelta].Add(lastpoint);

            //    }
            return toReturn;
        }
    }
}