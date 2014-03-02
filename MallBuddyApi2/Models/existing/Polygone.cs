using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.SqlTypes;
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
        public List<Area> Areas { get; set; }
        private List<Point3D> points;
        public virtual List<Point3D> Points
        {
            get { return points; }

            set
            {
                StringBuilder wktsb = new StringBuilder("POLYGON ((");
                foreach (Point3D point in value)
                    wktsb.Append(point.Longitude + " " + point.Latitude + ",");
                wktsb.Remove(wktsb.Length - 1, 1);
                wktsb.Append("))");
                try
                {
                    DbGeometry polygone = DbGeometry.PolygonFromText(wktsb.ToString(), 4326);
                    if (!polygone.IsValid)
                    {
                        this.LocationG = DbGeometry.FromText(SqlGeometry.STGeomFromText(new SqlChars(polygone.AsText()), 4326).MakeValid().STAsText().ToSqlString().ToString(), 4326);
                    }
                }
                catch (Exception ex)
                {
                    ex = ex;
                }
                this.Wkt = wktsb.ToString();
                points = value;
            }
        }

        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public DbGeometry LocationG { get; set; }
        public bool Accessible { get; set; }
        //public 

        public override bool Equals(Object other)
        {
            if (other.GetType() != typeof(Polygone))
                return false;
            return (this.Wkt == ((Polygone)other).Wkt && this.Level == ((Polygone)other).Level);
        }

        public override int GetHashCode()
        {
            return LocationG != null ? LocationG.GetHashCode() : (String.IsNullOrEmpty(Wkt) ? Id : Wkt.GetHashCode());
        }
    }
}
