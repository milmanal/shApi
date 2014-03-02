using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShimebaMvcAPI.Models
{
    public class POI
    {
        public String id { get; set; }
        public long dbID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public LocationDetails location { get; set; }
        public Point3D anchor { get; set; }
        public bool isWalkable { get; set; }
        public List<Image> imageList { get; set; }
        public String imageUrl { get; set; }
        public POIType type { get; set; }

        public enum POIType { STORE, PASSAGE, ELEVATOR, ATM, WC, STAIRS, MOVING_STAIRS, ENTRANCE, EXIT, PARKING, KIOSK }

    }
}
