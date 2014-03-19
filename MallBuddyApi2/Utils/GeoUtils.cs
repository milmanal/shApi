using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Utils
{
    public class GeoUtils
    {

        public static double GetHaversineDistance(Point3D source, Point3D target)
        {
            //double R = (type == DistanceType.Miles) ? 3960 : 6371;
 
            double dLat = ToRad(Decimal.ToDouble(target.Latitude - source.Latitude));
            double dLon = ToRad(Decimal.ToDouble(target.Longitude - source.Longitude));
 
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(Decimal.ToDouble(source.Latitude))) *Math.Cos(ToRad(Decimal.ToDouble(target.Latitude))) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = EARTH_RADIUS_KM * c;
 
            return d;
        }

        public static bool IsPointInPolygone(Point3D point, Polygone polygone)
        {
            int pointsCount = polygone.Points.Count;
            bool c = false;
            for (int i = 0, j = pointsCount - 1; i < pointsCount; j = i++)
            {
                if (((polygone.Points[i].Latitude > point.Latitude) != (polygone.Points[j].Latitude > point.Latitude)) &&
                    (point.Longitude < (polygone.Points[j].Longitude - polygone.Points[i].Longitude) * (point.Latitude - polygone.Points[i].Latitude) / (polygone.Points[j].Latitude - polygone.Points[i].Latitude) + polygone.Points[i].Longitude))
                    c = !c;
            }
            return c;
        }

        /*
         * int pnpoly(int nvert, float *vertx, float *verty, float testx, float testy)
{
  int i, j, c = 0;
  for (i = 0, j = nvert-1; i < nvert; j = i++) {
    if ( ((verty[i]>testy) != (verty[j]>testy)) &&
     (testx < (vertx[j]-vertx[i]) * (testy-verty[i]) / (verty[j]-verty[i]) + vertx[i]) )
       c = !c;
  }
  return c;
}
         */
        /// <summary>
        /// Radius of the Earth in Kilometers.
        /// </summary>

        private const double EARTH_RADIUS_KM = 6371;

        /// <summary>
        /// Converts an angle to a radian.
        /// </summary>
        /// <param name="input">The angle that is to be converted.</param>
        /// <returns>The angle in radians.</returns>
        private static double ToRad(double input)
        {
            return input * (Math.PI / 180);
        }







    }
}