﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity.Spatial;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MallBuddyApi.Models
{
    public class POI
    {
        //public String Id { get; set; }
        [Key]
        public long DbID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual Polygone Location { get; set; }                
        [NotMapped]
        [JsonIgnore]
        public virtual Point3D Anchor { get; set; }       
        public bool IsWalkable { get; set; }
        public List<Image> ImageList { get; set; }
        public String ImageUrl { get; set; }
        [EnumDataType( typeof(POIType))]
        public Nullable<POIType> Type { get; set; }
        public bool Enabled { get; set; }
        public enum POIType { STORE, PASSAGE, ELEVATOR, ATM, WC, STAIRS, ESCALATOR, ENTRANCE, PARKING, KIOSK, ZONE }

    }
}
