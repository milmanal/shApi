using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace MallBuddyApi2.Models
{
    public class Point3D
    {
        public long Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public DbGeometry LocationG { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public String Wkt { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public List<Area> Areas { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public bool IsAccessible { get; set; }
    }
}
