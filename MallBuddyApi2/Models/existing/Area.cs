using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MallBuddyApi2.Models
{
    public class Area : IEquatable<Area>
    {
        public Area() { }
        public Area(string area)
        {
            this.AreaID = area;
        }
        [Key]
        //[JsonIgnore]
        //[XmlIgnore]
        // public int ID { get; set; }
        public String AreaID { get; set; }

        public bool Equals(Area other)
        {
            return AreaID == other.AreaID;
        }

        public override int GetHashCode()
        {
            return AreaID.GetHashCode();
        }
    }
}
