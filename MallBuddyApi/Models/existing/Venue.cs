using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi.Models
{
    public class Venue
    {
        public int ID { get; set; }
        public virtual List<POI> PointsOfInterest { get; set; }

    }
}