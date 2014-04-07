//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////using QuickGraph;
//using Newtonsoft.Json;
//using System.Runtime.Serialization;
//using System.Data.Entity.Spatial;
//using MallBuddyApi2.Utils;

//namespace MallBuddyApi2.Models.existing
//{
//    public enum ConnectorType { NONE = 0, PATH, STAIRS, ELEVATOR, SINGLE_ESCALATOR, DOUBLE_ESCALATOR, TELEPORT }

//    public class LineString3D<T> : IEdge<T> where T : Point3D
//    {
//        public long Id { get; set; }
//        public T Source { get; set; }
//        public T Target { get; set; }
//        public int Level { get; set; }
//        [JsonIgnore]
//        [IgnoreDataMemberAttribute]
//        public DbGeometry LocationG { get; set; }
//        [JsonIgnore]
//        [IgnoreDataMemberAttribute]
//        public String Wkt { get; set; }
//        public string Name { get; set; }
//        [JsonIgnore]
//        [IgnoreDataMemberAttribute]
//        public bool IsAccessible { get; set; }
//        public bool BiDirectional { get; set; }
//        public ConnectorType connectorType { get; set; }

//        private double distance;
//        public double Distance        
//        {
//            get 
//            {
//                if (distance!=0)
//                    return distance;
//                if (Source == null || Source.LocationG == null || Target == null || Target.LocationG == null)
//                    return 0;
//                double? result = GeoUtils.GetHaversineDistance(Source, Target);
//                distance = (result != null ? (double)result : 0);
//                return distance;                
//            }
//            set
//            {
//                distance = value;
//            }
//        }

//        public LineStringDTO toDTO()
//        {
//            LineStringDTO toRet = new LineStringDTO
//            {
//                Id = this.Id,
//                Distance = this.distance,
//                Wkt = this.Wkt,
//                LocationG = this.LocationG,
//                Name = this.Name,
//                Target = this.Target,
//                Source = this.Source,
//                IsAccessible = this.IsAccessible,
//                Level = this.Level,
//                BiDirectional = this.BiDirectional,
//                connectorType = this.connectorType
//            };
//            return toRet;
//        }

//        public void setWktAndLocationG()
//        {
//            Wkt = "LINESTRING(" + Source.Longitude + " " + Source.Latitude + "," + Target.Longitude + " " + Target.Latitude + ")";
//            LocationG = DbGeometry.LineFromText(Wkt, 4326);
//        }
//        //public Point3D Source
//        //{
//        //    get { return ; }
//        //}

//        //public Point3D Target
//        //{
//        //    get { throw new NotImplementedException(); }
//        //}


//        internal RoutingStep toRoutingStep()
//        {
//            RoutingStep step = new RoutingStep
//            {
//                Destination = Target,
//                Source = Source,
//                Distance = Distance,
//                Name = Name,
//                connectorType = this.connectorType
//            };
//            //if (Target.Type == Point3D.PointType.LEVEL_CONNECTION)
//                // add instructions implementation - which floor to press etc. what floor are we going too if 
//                // its floor change;
//            if (connectorType == ConnectorType.ELEVATOR)
//            {
//                step.Instructions = "Go to floor " + Target.Level / 2 + " in the elevator";
//            }

//            if (connectorType == ConnectorType.STAIRS | connectorType == ConnectorType.ELEVATOR | connectorType == ConnectorType.DOUBLE_ESCALATOR | connectorType == ConnectorType.SINGLE_ESCALATOR)
//            {
//                if (Source.Level - Target.Level == 2)
//                    step.Instructions = "Take the "+connectorType.ToString()+" down 1 level";
//                if (Source.Level > Target.Level)
//                    step.Instructions = "Take the " + connectorType.ToString() + " down " + (Source.Level - Target.Level) / 2 + " levels";
//                if (Target.Level - Source.Level == 2)
//                    step.Instructions = "Take the " + connectorType.ToString() + " up 1 level";
//                if (Target.Level > Source.Level)
//                    step.Instructions = "Take the " + connectorType.ToString() + " up " + (Target.Level - Source.Level) / 2 + " levels";
//            }

//            return step;
//        }
//    }
//}
