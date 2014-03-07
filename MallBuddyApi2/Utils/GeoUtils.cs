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