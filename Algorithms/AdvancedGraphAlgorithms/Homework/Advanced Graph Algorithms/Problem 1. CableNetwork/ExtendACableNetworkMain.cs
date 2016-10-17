namespace Problem_1.CableNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ExtendACableNetworkMain
    {
        static List<Edge> edges;
        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            int nodesCount = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            int edgesCount = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            var spanningTreeNodes = new HashSet<int>();
            var spanningTreeEdges = new List<Edge>();
            var extendedTreeEdges = new List<Edge>();     
            edges = new List<Edge>();

            int edgeIndex = 1;
            while (edgeIndex <= edgesCount)
            {
                string[] parameters = Console.ReadLine().Split();
                int startNode = int.Parse(parameters[0]);
                int endNode = int.Parse(parameters[1]);
                int distance = int.Parse(parameters[2]);
                
                Edge edge = new Edge(startNode, endNode, distance);
                edges.Add(edge);
                if (parameters.Length == 4 && parameters[3].Equals("connected"))
                {
                    spanningTreeNodes.Add(startNode);
                    spanningTreeNodes.Add(endNode);
                    spanningTreeEdges.Add(edge);
                }

                edgeIndex++;
            }

            var graph = BuildGraph();

            int usedBudget = Prim(graph, spanningTreeNodes, extendedTreeEdges, spanningTreeEdges, budget);
            foreach (var edge in extendedTreeEdges)
            {
                Console.WriteLine(edge);
            }

            Console.WriteLine("Budget used: " + usedBudget);
        }

        private static int Prim(Dictionary<int, List<Edge>> graph,
        HashSet<int> spanningTreeNodes, List<Edge> extendedTreeEdges,
        List<Edge> spanningTreeEdges, int maxBudget)
        {
            int budget = 0;
            var priorityQueue = new BinaryHeap<Edge>();
            List<Edge> edgesWithNoConnection = new List<Edge>();
            //First enqueue in priorityQueue all edges with one node already connected 
            //and in edgesWithNoConnection all edges with no connection 
            foreach (var node in graph.Keys)
            {
                foreach (var edge in graph[node])
                {
                    if (!(spanningTreeNodes.Contains(edge.StartNode) && spanningTreeNodes.Contains(edge.EndNode)))
                    {
                        if (!(spanningTreeNodes.Contains(edge.StartNode) || spanningTreeNodes.Contains(edge.EndNode)))
                        {
                            edgesWithNoConnection.Add(edge);
                        }
                        else
                        {
                            priorityQueue.Enqueue(edge);
                        }
                    }                        
                }
            }
        
            while (priorityQueue.Count > 0 && budget < maxBudget)
            {
                var smallestEdge = priorityQueue.ExtractMin();
                if(budget + smallestEdge.Cost > maxBudget)
                {
                    break;
                }

                if (spanningTreeNodes.Contains(smallestEdge.StartNode)
                    ^ spanningTreeNodes.Contains(smallestEdge.EndNode))
                {
                    var nonTreeNode = spanningTreeNodes.Contains(smallestEdge.StartNode)
                    ? smallestEdge.EndNode
                    : smallestEdge.StartNode;
                    spanningTreeEdges.Add(smallestEdge);
                    spanningTreeNodes.Add(nonTreeNode);
                    extendedTreeEdges.Add(smallestEdge);
                    budget += smallestEdge.Cost;
                    //After adding new edge new all edges containing nontreenode from edgesWithNoConnection are added back to priority queue
                    foreach (var edge in edgesWithNoConnection.Intersect(graph[nonTreeNode]))
                    {
                        priorityQueue.Enqueue(edge);
                    }
                }
            }

            return budget;
        }

        static Dictionary<int, List<Edge>> BuildGraph()
        {
            var graph = new Dictionary<int, List<Edge>>();
            foreach (var edge in edges)
            {
                if (!graph.ContainsKey(edge.StartNode))
                {
                    graph.Add(edge.StartNode, new List<Edge>());
                }
                graph[edge.StartNode].Add(edge);
                if (!graph.ContainsKey(edge.EndNode))
                {
                    graph.Add(edge.EndNode, new List<Edge>());
                }
                graph[edge.EndNode].Add(edge);
            }

            return graph;
        }
    }
}
