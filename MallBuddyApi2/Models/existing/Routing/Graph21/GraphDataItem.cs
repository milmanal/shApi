using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing.Routing.Graph21
{
    public class GraphDataItem
    {
        public int EdgeID { get; set; }
        public int SourceVertexID { get; set; }
        public int TargetVertexID { get; set; }
        public double Cost { get; set; }
        public double ReverseCost { get; set; }
        public bool IsReverse { get; set; }
        public int Num { get; set; }

        public GraphDataItem Clone()
        {
            return new GraphDataItem
            {
                EdgeID = EdgeID,
                SourceVertexID = SourceVertexID,
                TargetVertexID = TargetVertexID,
                Cost = Cost,
                ReverseCost = ReverseCost,
            };
        }

        public string Name { get; set; }
    }
}
