using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;

namespace MallBuddyApi.Models
{
    public class Polygone
    {
        public int Id { get; set; }
        [JsonIgnore]
        public String Wkt { get; set; }
        //public List<Area> areas { get; set; }
        public int Level { get; set; }
        public virtual List<Point3D> Points { get; set; }

        public virtual List<Area> Areas { get; set; }
        [JsonIgnore]
        public DbGeometry LocationG { get; set; }
        public bool Accessible { get; set; }
        //public 
    }
}
