using System.Collections.Generic;
using MallBuddyApi2.Models.existing.Routing;

public class Vertex
{
    public Vertex()
    {
        Neighbours = new List<Vertex>();
        Cost = double.PositiveInfinity;
    }

    public int ID { get; set; }
    public bool Visited { get; set; }
    public double Cost { get; set; }
    public Vertex PreviousVertex { get; set; }
    public Edge PreviousEdge { get; set; }
    public List<Vertex> Neighbours { get; private set; }
}