//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using QuickGraph;
//using QuickGraph.Algorithms.ShortestPath;
//using QuickGraph.Algorithms.Observers;

//namespace MallBuddyApi2.Models.existing
//{
//    public class Graph
//    {
//        Point3D root;
//        Point3D target;
//        UndirectedGraph<Point3D, LineString3D<Point3D>> graph;
//        IVertexListGraph<Point3D, LineString3D<Point3D>> agraph;

//        UndirectedDijkstraShortestPathAlgorithm<Point3D, LineString3D<Point3D>> dijkstra;
//        AStarShortestPathAlgorithm<Point3D, LineString3D<Point3D>> astar;

//        public Graph (IEnumerable<LineString3D<Point3D>> edges)
//        {
//            //IEdge<Point3D>[] edges = new LineString3D<Point3D>[] { };
//            graph = new UndirectedGraph<Point3D, LineString3D<Point3D>>(true);
//            graph.AddVerticesAndEdgeRange(edges);
//            //agraph = new 
//            //var graph = edges.ToUndirectedGraph<Point3D, LineString3D<Point3D>>();
//            // We want to use Dijkstra on this graph
//            //Dictionary<LineString3D<Point3D>, double> edgeCost = new Dictionary<LineString3D<Point3D>, double>(graph.EdgeCount);

//           // DijkstraShortestPathAlgorithm<Point3D, LineString3D<Point3D>> dijkstra = new DijkstraShortestPathAlgorithm<Point3D, LineString3D<Point3D>>(graph, edgeCost);
//        }

//        public RoutingPath GetDijkstraPath(Point3D root, Point3D target)
//        {
//            this.root = root;
//            this.target = target;
//            Func<LineString3D<Point3D>, double> getWeight = edge => edge.Distance;
//            this.dijkstra = new UndirectedDijkstraShortestPathAlgorithm<Point3D, LineString3D<Point3D>>(graph, getWeight);

//            //// attach a distance observer to give us the shortest path distances
//            UndirectedVertexDistanceRecorderObserver<Point3D, LineString3D<Point3D>> distObserver = new UndirectedVertexDistanceRecorderObserver<Point3D, LineString3D<Point3D>>(getWeight);
//            dijkstra.DiscoverVertex +=dijkstra_DiscoverVertex;
//            using (distObserver.Attach(dijkstra))
//            {

//                //// Attach a Vertex Predecessor Recorder Observer to give us the paths
//                UndirectedVertexPredecessorRecorderObserver<Point3D, LineString3D<Point3D>> predecessorObserver = new UndirectedVertexPredecessorRecorderObserver<Point3D, LineString3D<Point3D>>();
//                using (predecessorObserver.Attach(dijkstra))
//                {
//                    //// Run the algorithm with A set to be the source
//                    dijkstra.Compute(root);

//                    RoutingPath path = new RoutingPath { Source = root, Destination = target, Routingsteps = new List<RoutingStep>() };
                                        
//                    //foreach (KeyValuePair<Point3D, double> kvp in distObserver.Distances)
//                    //    Console.WriteLine("Distance from root to node {0} is {1}", kvp.Key, kvp.Value);
//                    PointsInPath points = new PointsInPath { points = new List<Point3D>() };
                    
//                    foreach (KeyValuePair<Point3D, LineString3D<Point3D>> kvp in predecessorObserver.VertexPredecessors)
//                    {
//                        points.points.Add(kvp.Key);
//                        RoutingStep step = kvp.Value.toRoutingStep();
//                        path.Routingsteps.Add(step);
//                        path.Distance += step.Distance;
//                    }
//                    return path;
//                }
//            }
//        }

//        public RoutingPath GetAstarPath(Point3D root, Point3D target)
//        {
//            this.root = root;
//            this.target = target;
//            Func<LineString3D<Point3D>, double> getWeight = edge => edge.Distance;
//            this.astar = new AStarShortestPathAlgorithm<Point3D, LineString3D<Point3D>>(agraph, getWeight, x => 1);

//            //// attach a distance observer to give us the shortest path distances
//            UndirectedVertexDistanceRecorderObserver<Point3D, LineString3D<Point3D>> distObserver = new UndirectedVertexDistanceRecorderObserver<Point3D, LineString3D<Point3D>>(getWeight);
//            dijkstra.DiscoverVertex += dijkstra_DiscoverVertex;
//            using (distObserver.Attach(dijkstra))
//            {

//                //// Attach a Vertex Predecessor Recorder Observer to give us the paths
//                UndirectedVertexPredecessorRecorderObserver<Point3D, LineString3D<Point3D>> predecessorObserver = new UndirectedVertexPredecessorRecorderObserver<Point3D, LineString3D<Point3D>>();
//                using (predecessorObserver.Attach(dijkstra))
//                {
//                    //// Run the algorithm with A set to be the source
//                    dijkstra.Compute(root);

//                    RoutingPath path = new RoutingPath { Source = root, Destination = target, Routingsteps = new List<RoutingStep>() };

//                    //foreach (KeyValuePair<Point3D, double> kvp in distObserver.Distances)
//                    //    Console.WriteLine("Distance from root to node {0} is {1}", kvp.Key, kvp.Value);
//                    PointsInPath points = new PointsInPath { points = new List<Point3D>() };

//                    foreach (KeyValuePair<Point3D, LineString3D<Point3D>> kvp in predecessorObserver.VertexPredecessors)
//                    {
//                        points.points.Add(kvp.Key);
//                        RoutingStep step = kvp.Value.toRoutingStep();
//                        path.Routingsteps.Add(step);
//                        path.Distance += step.Distance;
//                    }
//                    return path;
//                }
//            }
//        }

//        private void dijkstra_DiscoverVertex(Point3D vertex)
//        {
//            if (vertex == target)
//                dijkstra.Abort();
//        }
//    }
//}