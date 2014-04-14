using MallBuddyApi2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public class DrawablePOI
    {
        public string Name { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public string LogoUrl { get; set; }

        public DrawablePOI() { }

        public DrawablePOI (POI poi)
        {
            Name = poi.Name;
            if (poi is Store)
                LogoUrl = ((Store)poi).LogoUrl;
            if (poi.Location!=null)
                calculateCenter (poi.Location);
        }

        private void calculateCenter(Polygone location)
        {
            MallBuddyApi2.Utils.GeoUtils.CGPoint point = GeoUtils.longLat2PixelPoint( (double)location.LocationG.Centroid.XCoordinate, (double)location.LocationG.Centroid.YCoordinate);
            CenterX = point.x;
            CenterY = point.y;
        }
    }
}
