using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class FloorMap
    {
        [Key, Column(Order = 0)]
        public int Level { get; set; }
        public string ImageNorthUrl { get; set; }
        public string ImageSouthUrl { get; set; }
        public string ImageWestUrl { get; set; }
        public string ImageEastUrl { get; set; }
        [Key, Column(Order = 1)]
        public string Mapsversion { get; set; }
        public DateTime Modified { get; set; }

    }
}