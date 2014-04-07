using log4net;
using MallBuddyApi2.Models.existing.Routing.Graph21;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing.Routing
{
    public class Graph2
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(Graph2));
        
     #region Class level variables
 
    private Dictionary<int, int> mVertexDictionary = new Dictionary<int, int>();
    private Dictionary<double, Edge> mEdges = new Dictionary<double, Edge>();
    private Dictionary<int, Vertex> mVertices = new Dictionary<int, Vertex>();
    private SortedList<double, List<Vertex>> mNextVertices = new SortedList<double, List<Vertex>>();
    private int mVertexCounter = 1;
    private int mTries = 0;
 
    private int mSourceVertexID;
    private int mTargetVertexID;
 
    #endregion
 
    #region Constructor
 
    public Graph2(IEnumerable<GraphDataItem> items)
    {
        foreach (var item in items)
        {
            CreateEdge(item);
        }
    }
 
    #endregion
 
    #region Factory Methods
 
    private static Graph2 Create(IEnumerable<GraphDataItem> items)
    {
        return new Graph2(items);
    }
 
    #endregion
 
    #region Methods
 
    public GraphSearchResult GetShortestPath(int sourceVertexID, int targetVertexID)
    {
        mSourceVertexID = mVertexDictionary[sourceVertexID];
        mTargetVertexID = mVertexDictionary[targetVertexID];
 
        mTries = 0;
 
        var stopwatch = new Stopwatch();
        //stopwatch.Start();
        var vertex = GetShortestPathUsingDijkstraAlgorithm();
        //stopwatch.Stop();
        var result = new GraphSearchResult(vertex, stopwatch.Elapsed, mTries);
 
        ResetVertices();
 
        return result;
    }
 
    #region Dijkstra algorithm
 
    private Vertex GetShortestPathUsingDijkstraAlgorithm()
    {
        //logger.Info("calculating dijkstra from vertexid: " + mSourceVertexID + " to target vertexid: " + mTargetVertexID);
        var current = mVertices[mSourceVertexID];
        current.Cost = 0;
        mNextVertices.Clear();
        var unvisited = mVertices.Count;
        while (current != null && unvisited > 0)
        {
            mTries++;
            if (current.ID == mTargetVertexID)
            {
                return current;
            }
            foreach (var neighbour in current.Neighbours)
            {
                if (!neighbour.Visited)
                {
                    var edge = GetEdge(current.ID, neighbour.ID);
                    //logger.Info("Edge is: " + edge.name+" ,Vertexes:"+current.ID+","+neighbour.ID+" cost:"+edge.Cost);
                    var totalCost = current.Cost + edge.Cost;
                    if (totalCost < neighbour.Cost)
                    {
                        RemoveNextVertex(neighbour);
                        neighbour.Cost = totalCost;
                        neighbour.PreviousVertex = current;
                        neighbour.PreviousEdge = edge;
                        AddNextVertex(neighbour);
                    }
                }
            }
 
            //mark Vertex visited
            current.Visited = true;
            RemoveNextVertex(current);
 
            current = GetNextVertex();
            //if (current!=null)
                //logger.Info("Next vertex is: " + current.ID + " cost:" + current.Cost + " neighbours:" + current.Neighbours.Count);
        }
        return null;
    }
 
    private Vertex GetNextVertex()
    {
        if (mNextVertices.Count > 0)
        {
            var dist = mNextVertices.Values[0];
            return dist != null ? dist[0] : null;

        }
        return null;
    }
 
    private void AddNextVertex(Vertex neighbour)
    {
        var cost = neighbour.Cost;
        if (mNextVertices.ContainsKey(cost))
        {
            var dist = mNextVertices[cost];
            if (dist == null)
            {
                mNextVertices[cost] = new List<Vertex>()
                {
                    neighbour
                };
                //logger.Info("added vertex: " + neighbour.ID + " to next vertices with existing cost " + neighbour.Cost);
                //logger.Info("next vertices has " + mNextVertices.Count + " costs now");
            }
            else
            {
                dist.Add(neighbour);
            }
        }
        else
        {
            mNextVertices.Add(cost, new List<Vertex>()
            {
                neighbour
            });
            //logger.Info("added vertex: " + neighbour.ID + " to next vertices with new cost " + neighbour.Cost);
            //logger.Info("next vertices has " + mNextVertices.Count + " costs now");
        }
    }
 
    private void RemoveNextVertex(Vertex neighbour)
    {
        var cost = neighbour.Cost;
        if (mNextVertices.ContainsKey(cost))
        {
            var dist = mNextVertices[cost];
            if (dist != null)
            {
                dist.Remove(neighbour);
                //logger.Info("removing vertex: " + neighbour.ID + " from next vertices with existing cost " + neighbour.Cost);
                if (dist.Count == 0)
                {
                    mNextVertices.Remove(cost);
                    //logger.Info("removing cost " + neighbour.Cost);

                }
            }
        }
    }
 
    private void ResetVertices()
    {
        foreach (var item in mVertices.Values)
        {
            item.Visited = false;
            item.Cost = double.PositiveInfinity;
            item.PreviousVertex = null;
            item.PreviousEdge = null;
        }
    }
 
    #endregion
 
    #region Initialization
 
    private void CreateEdge(GraphDataItem item)
    {
        var edge = new Edge
        {
            ID = item.EdgeID,
            SourceVertex = EnsureVertex(item.SourceVertexID),
            TargetVertex = EnsureVertex(item.TargetVertexID),
            Cost = item.Cost,
            IsReverse = false,
            name = item.Name,
            DataItem = item,
        };
 
        edge.SourceVertex.Neighbours.Add(edge.TargetVertex);
        {
            var key = GetKey(edge.SourceVertex.ID, edge.TargetVertex.ID);
            if (!mEdges.ContainsKey(key))
            {
                mEdges.Add(key, edge);
            }
            else if (mEdges[key].Cost > edge.Cost)
            {
                mEdges[key] = edge;
            }
        }
 
        edge = new Edge
        {
            ID = item.EdgeID,
            SourceVertex = EnsureVertex(item.TargetVertexID),
            TargetVertex = EnsureVertex(item.SourceVertexID),
            Cost = item.ReverseCost,
            IsReverse = true,
            DataItem = item,
            name = item.Name,
        };
        edge.SourceVertex.Neighbours.Add(edge.TargetVertex);
        {
            var key = GetKey(edge.SourceVertex.ID, edge.TargetVertex.ID);
            if (!mEdges.ContainsKey(key))
            {
                mEdges.Add(key, edge);
            }
            else if (mEdges[key].Cost > edge.Cost)
            {
                mEdges[key] = edge;
            }
        }
    }
 
    private Vertex EnsureVertex(int originalVertexID)
    {
        if (mVertexDictionary.ContainsKey(originalVertexID))
        {
            return mVertices[mVertexDictionary[originalVertexID]];
        }
        var newVertexID = mVertexCounter;
        var newVertex = new Vertex { ID = newVertexID };
        mVertexCounter++;
 
        mVertices.Add(newVertexID, newVertex);
        mVertexDictionary.Add(originalVertexID, newVertexID);
 
        return newVertex;
    }
 
    private double GetKey(int start, int end)
    {
        // we support 10000000 nodes graph - I dont think we need or can handle more
        return start * 10000000 + end;
    }
 
    private Edge GetEdge(int startVertexID, int endVertexID)
    {
        return mEdges[GetKey(startVertexID, endVertexID)];
    }
 
    #endregion
 
    #endregion
    }
}