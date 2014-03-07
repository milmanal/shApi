using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using MallBuddyApi2.Models.existing;
using System.ComponentModel.DataAnnotations;

namespace MallBuddyApi2.Models
{
    public class Point3D : IEquatable<Point3D>
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
        [JsonIgnore]
        [IgnoreDataMemberAttribute]

        [EnumDataType(typeof(PointType))]
        public PointType Type { get; set; }

        public enum PointType {  UNDEFINED=0, ENTRANCE, PATH_POINT, LEVEL_CONNECTION}


        public override bool Equals(object obj)
        {
            if (!(obj is Point3D))
                return false;
            if (string.IsNullOrEmpty(this.Wkt) | string.IsNullOrEmpty(obj.ToString()))
                return false;
            return this.Wkt == ((Point3D)obj).Wkt && this.Level == ((Point3D)obj).Level;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.Wkt))
                return base.GetHashCode();
            return (Level.ToString() + "_" + Wkt).GetHashCode();
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Longitude == b.Longitude && a.Latitude == b.Latitude && a.Level == b.Level;
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return !(a == b);
        }



        public bool Equals(Point3D other)
        {
            if (other == null)
            {
                return false;
            }
            // Return true if the fields match:
            return Longitude == other.Longitude && Latitude == other.Latitude && Level == other.Level;
        }

        internal void setWktAndGeometry()
        {
            Wkt = "POINT(" + Longitude + " " + Latitude + ")";
            LocationG = DbGeometry.PointFromText(Wkt, 4326);
        }

        public string toGeoJson()
        {
            return "{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":" +
                "[" + Longitude + "," + Latitude + "]},\"properties\":{\"level\":" + Level + "}}";

        }
    }


}

