using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi.Models.existing
{
    public class Connector
    {
        public int Id {get; set;}
        public int StartLevel { get; set; }
        public int EndLevel { get; set; }
        public Point3D Anchor { get; set; }
        public ConnectorType Type { get; set; }
        //public string AnchorWKT 
    }

    public enum ConnectorType { STAIRS, ELEVATOR, ESCALATOR_SINGLE, ESCALATOR_DOUBLE, TELEPORT}
    
}