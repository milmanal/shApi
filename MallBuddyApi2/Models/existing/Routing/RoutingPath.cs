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
            RoutingPathByFloor toReturn = new RoutingPathByFloor 
            { 
                Source = this.Source, 
                Destination = this.Destination, 
                Distance = this.Distance, 
                PointsOfFloors = new Dictionary<int, List<RoutingPoint>>() 
            };
            double coveredDistance = 0;
            int floorsDirectionDelta = Source.Level >= Destination.Level ? -1 : 1;
            // treat the first point separately
            if (!toReturn.PointsOfFloors.ContainsKey(Source.Level))
                toReturn.PointsOfFloors[Source.Level] = new List<RoutingPoint>();
            RoutingPoint firstpoint = Routingsteps[0].toRoutingPoint(coveredDistance, true);
            firstpoint.DistanceCovered = 0;
            toReturn.PointsOfFloors[Source.Level].Add(firstpoint);
            coveredDistance = firstpoint.DistanceCovered;
            if (hostedLevels.ContainsKey(Source.Level + floorsDirectionDelta) && hostedLevels[Source.Level + floorsDirectionDelta] != null)
                foreach (POI poi in hostedLevels[Source.Level + floorsDirectionDelta])
                {
                    int levelIndex = int.Parse(poi.Name[5].ToString());
                    if (levelIndex == Source.Level && poi.Location.LocationG.Contains(Source.LocationG))
                    {
                        if (!toReturn.PointsOfFloors.ContainsKey(Source.Level + floorsDirectionDelta))
                            toReturn.PointsOfFloors[Source.Level + floorsDirectionDelta] = new List<RoutingPoint>();
                        toReturn.PointsOfFloors[Source.Level + floorsDirectionDelta].Add(firstpoint);
                    }
                }
            RoutingPoint point = null;
            foreach (var step in this.Routingsteps)
            {
                int levelToAdd =  step.Source.Level;
                if (!string.IsNullOrEmpty(step.Instructions))
                {
                    levelToAdd = step.Destination.Level;
                    point.Instructions = step.Instructions;
                }
                if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd))
                    toReturn.PointsOfFloors[levelToAdd] = new List<RoutingPoint>();
                point = step.toRoutingPoint(coveredDistance, false);
                //if (step == Routingsteps[0])
                  //  continue;
                toReturn.PointsOfFloors[levelToAdd].Add(point);
                coveredDistance = point.DistanceCovered;
                if (hostedLevels.ContainsKey(levelToAdd + floorsDirectionDelta) && hostedLevels[levelToAdd + floorsDirectionDelta] != null)
                    foreach (POI poi in hostedLevels[levelToAdd + floorsDirectionDelta])
                    {
                        int levelIndex = int.Parse(poi.Name[5].ToString());
                        if (levelIndex == levelToAdd && poi.Location.LocationG.Contains(step.Source.LocationG))
                        {
                            if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd + floorsDirectionDelta))
                                toReturn.PointsOfFloors[levelToAdd + floorsDirectionDelta] = new List<RoutingPoint>();
                            toReturn.PointsOfFloors[levelToAdd + floorsDirectionDelta].Add(point);
                        }
                    }
                if (hostedLevels.ContainsKey(levelToAdd - floorsDirectionDelta) && hostedLevels[levelToAdd - floorsDirectionDelta] != null)
                    foreach (POI poi in hostedLevels[levelToAdd - floorsDirectionDelta])
                    {
                        int levelIndex = int.Parse(poi.Name[5].ToString());
                        if (levelIndex == levelToAdd && poi.Location.LocationG.Contains(step.Source.LocationG))
                        {
                            if (!toReturn.PointsOfFloors.ContainsKey(levelToAdd - floorsDirectionDelta))
                                toReturn.PointsOfFloors[levelToAdd - floorsDirectionDelta] = new List<RoutingPoint>();
                            toReturn.PointsOfFloors[levelToAdd - floorsDirectionDelta].Add(point);
                        }
                    }
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