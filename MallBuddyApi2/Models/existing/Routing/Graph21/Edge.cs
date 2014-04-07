using MallBuddyApi2.Models.existing.Routing;
using MallBuddyApi2.Models.existing.Routing.Graph21;

public class Edge
{
    public int ID { get; set; }
    public string name { get; set; }
    public Vertex SourceVertex { get; set; }
    public Vertex TargetVertex { get; set; }
    public double Cost { get; set; }
    public bool IsReverse { get; set; }
    public GraphDataItem DataItem { get; set; }
}