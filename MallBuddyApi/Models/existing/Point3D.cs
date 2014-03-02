using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;

namespace MallBuddyApi.Models
{
    public class Point3D
    {
        public long Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        public DbGeometry LocationG { get; set; }
        [JsonIgnore]
        public String Wkt { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        [JsonIgnore]
        public List<Area> Areas { get; set; }
        [JsonIgnore]
        public bool IsAccessible { get; set; }
    }
}
