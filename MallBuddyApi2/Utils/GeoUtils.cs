using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Utils
{
    public class GeoUtils
    {
        public static int bitmapOffsetX = 918;
        public static int bitmapOffsetY = 2387;
        public static double bitmapLocationLatitude = 32.0749904;
        public static double bitmapLocationLongitude = 34.7758688;
        public static double pixelsPerMeter = 12.2685;
        public static double bitmapOrientation = -420.6029;
        protected static double a = 6378137;
	    /** Equatorial earth radius */
	    protected static double b = 6356752.314245;
	    /** Polar earth radius */
	    protected static double r = 0.5 * (a + b);

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

        public static Point3D pixelPoint2LongLat(int x, int y, int floorNr)
        {
            // This function makes a local approximation and should not be used over
            // long distances
            // Transform to a coordinate system in upper left corner of image
            //FloorInfo f = getFloorInfo(floorNr);
            double deltaX = (bitmapOffsetY - y) / pixelsPerMeter;
            double deltaY = (bitmapOffsetX - x) / pixelsPerMeter;
            // Rotate to NW-system
            double deltaN = Math.Cos(bitmapOrientation * Math.PI / 180.0)
                    * deltaX + Math.Sin(bitmapOrientation * Math.PI / 180.0)
                    * deltaY;
            double deltaW = -Math.Sin(bitmapOrientation * Math.PI / 180.0)
                    * deltaX + Math.Cos(bitmapOrientation * Math.PI / 180.0)
                    * deltaY;

            decimal lat = (decimal) (bitmapLocationLatitude + 180.0 / Math.PI * deltaN / r);
            decimal lon = (decimal)(bitmapLocationLongitude - 180.0 / Math.PI * deltaW
                    / (r * Math.Cos(bitmapLocationLatitude * Math.PI / 180.0)));
            Point3D currentPos = new Point3D{Latitude= lat,Longitude= lon, Level= floorNr};
            return currentPos;
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