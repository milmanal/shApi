using ShimebaMvcAPI.Models.IndoorIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShimebaMvcAPI.Models.IndoorIODecomp
{
    class Geometry
    {
        public enum GeometryType { Line, LineString, Polygon, Point};
        public GeometryType type;
        public Coordinates[] coordinates;

    }
}
