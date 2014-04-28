using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity.Spatial;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using MallBuddyApi2.Models.existing;

namespace MallBuddyApi2.Models
{
    public class POI : IHasEntrances
    {
        //public String Id { get; set; }
        [Key]
        public long DbID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual Polygone Location { get; set; }
        public virtual List<Point3D> Entrances { get; set; }
        public int Level { get; set; }
        [NotMapped]
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public virtual Point3D Anchor { get; set; }
        //public bool IsWalkable { get; set; }
        //public List<Image> ImageList { get; set; }
        public String ImageUrl { get; set; }
        [EnumDataType(typeof(POIType))]
        public Nullable<POIType> Type { get; set; }
        public bool Enabled { get; set; }
        public enum POIType { STORE, PASSAGE, ELEVATOR, ATM, WC, STAIRS, ESCALATOR, ENTRANCE, PARKING, KIOSK, DEADZONE, HOSTED_LEVEL,SPECIAL_POI, NONE }
        public static string[] HebrewTypeMappings = {"חנות","מעבר","מעלית","כספומט","שירותים","מדרגות","מדרגות נעות","שער כניסה","מעבר לחניון","דוכן","אזור מת","מפלס אורח","שטח מיוחד","לא להצגה"};
        public DateTime Modified { get; set; }

        public virtual void LoadForDetails(ApplicationDbContext db)
        {
            db.Entry(this).Collection("Entrances").Load();
        }
    }
}
