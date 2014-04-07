using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing.Routing
{
    public class RoutingPoint
    {
        //public Point3D Point { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Level { get; set; }
        [JsonPropertyAttribute(NullValueHandling = NullValueHandling.Ignore)]
        public String Instructions { get; set; }
        [JsonPropertyAttribute(NullValueHandling = NullValueHandling.Ignore)]
        public String Name { get; set; }
        public double DistanceCovered { get; set; }
        public String ToGeojson()
        {
            // StringBuilder sb = new StringBuilder("{\"type\":\"Feature\",\"geometry\":{\"type\":\"LineString\",\"coordinates\":[");
            //     sb.Append("["+Source.Longitude+","+Source.Latitude+"],["+Destination.Longitude+","+Destination.Latitude+"]]},\"properties\":{\"level\":"+Source.Level+"}}");
            return @"{""type"":""Feature"",""geometry"":{""type"":""POINT"",""coordinates"":[" +
                 +Longitude + "," + Latitude + @"]},""properties"":{""level"":" + Level + "}}";
        }
    }
}