using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class MapsData
    {
        [Key]
        public string MapsVersion { get; set; }
        public List<FloorMap> FloorMaps { get; set; }
    }
}