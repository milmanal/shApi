using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class Connector
    {
        public int Id {get; set;}
        public Point3D Highpoint { get; set; }
        public Point3D Lowpoint { get; set; }
        //public Point3D Anchor { get; set; }
        public ConnectorType Type { get; set; }
        //public string AnchorWKT 
    }

    public enum ConnectorType { STAIRS, ELEVATOR, ESCALATOR_SINGLE, ESCALATOR_DOUBLE, TELEPORT}
    
}