using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class RoutingStep
    {
        public Point3D Source { get; set; }
        public Point3D Destination { get; set; }
        [JsonPropertyAttribute (NullValueHandling=NullValueHandling.Ignore)]
        public String Instructions { get; set; }
        public MallBuddyApi2.Models.existing.LineStringDTO.ConnectorType connectorType { get; set; }
        [JsonPropertyAttribute(NullValueHandling = NullValueHandling.Ignore)]
        public String Name { get; set; }
        public double Distance { get; set; }
        public String ToGeojson()
        {
            // StringBuilder sb = new StringBuilder("{\"type\":\"Feature\",\"geometry\":{\"type\":\"LineString\",\"coordinates\":[");
            //     sb.Append("["+Source.Longitude+","+Source.Latitude+"],["+Destination.Longitude+","+Destination.Latitude+"]]},\"properties\":{\"level\":"+Source.Level+"}}");
            return @"{""type"":""Feature"",""geometry"":{""type"":""LineString"",""coordinates"":[" +
                "[" + Source.Longitude + "," + Source.Latitude + "],[" + Destination.Longitude + "," + Destination.Latitude + @"]]},""properties"":{""level"":" + Source.Level + "}}";
        }

        internal void setSourceTargetDistance(Point3D from, Point3D to, double distance)
        {
            this.Source = from;
            this.Destination = to;
            this.Distance = distance;
        }

        internal Routing.RoutingPoint toRoutingPoint(double coveredDistance, bool isSource)
        {
            return new Routing.RoutingPoint
            {
                Name = isSource ? this.Source.Name : this.Destination.Name,
                Level = isSource ? this.Source.Level : this.Destination.Level,
                Latitude = isSource ? this.Source.Latitude : this.Destination.Latitude,
                Longitude = isSource ? this.Source.Longitude : this.Destination.Longitude,
                Instructions = this.Instructions,
                DistanceCovered = coveredDistance + this.Distance
            };
        }
    }
}