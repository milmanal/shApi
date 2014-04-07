using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing.Routing
{
    public class RoutingPathByFloor
    {
        public Point3D Source { get; set; }
        public Point3D Destination { get; set; }
        public Dictionary<int,List<RoutingPoint>> PointsOfFloors { get; set; }
        public double Distance { get; set; }
    }
}