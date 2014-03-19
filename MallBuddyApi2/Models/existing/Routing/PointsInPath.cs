using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class PointsInPath
    {
        public List<Point3D> points { get; set; }

        public string toGeoJson()
        {
            StringBuilder sb = new StringBuilder("{\"type\":\"FeatureCollection\",\"features\":[");
            foreach (Point3D rstep in points)
                sb.Append(rstep.toGeoJson() + ",");
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]}");
            return sb.ToString();
        }
    }
}