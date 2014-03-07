using MallBuddyApi2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    [Table ("LineStrings")]
    public class LineStringDTO
    {
        public long Id { get; set; }
        public Point3D Source { get; set; }
        public Point3D Target { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public DbGeometry LocationG { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public String Wkt { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMemberAttribute]
        public bool IsAccessible { get; set; }

        private double distance;
        public double Distance
        {
            get
            {
                if (distance != 0)
                    return distance;
                if (Source == null || Source.LocationG == null || Target == null || Target.LocationG == null)
                    return 0;
                double? result = GeoUtils.GetHaversineDistance(Source, Target);
                distance = (result != null ? (double)result : 0);
                return distance;
            }
            set
            {
                distance = value;
            }
        }

        public LineString3D<Point3D> toGeneric()
        {
            LineString3D<Point3D> toRet = new LineString3D<Point3D>
            {
                Id = this.Id,
                Distance = this.distance,
                Wkt = this.Wkt,
                LocationG = this.LocationG,                
                Name = this.Name,
                Target = this.Target,
                Source = this.Source,
                IsAccessible = this.IsAccessible,
                Level = this.Level
            };
            return toRet;
        }
    }
}