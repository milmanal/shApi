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
    public class Polygone
    {
        public int Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public String Wkt { get; set; }
        //public List<Area> areas { get; set; }
        public int Level { get; set; }
        public virtual List<Point3D> Points { get; set; }

        public virtual List<Area> Areas { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public DbGeometry LocationG { get; set; }
        public bool Accessible { get; set; }
        //public 
    }
}
