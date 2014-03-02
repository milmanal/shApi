using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MallBuddyApi.Models
{
    public class Area
    {
        public Area() { }
        public Area(string area)
        {
            this.AreaID = area;
        }
        [Key]
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public int ID { get; set; }
        public String AreaID { get; set; }
    }
}
