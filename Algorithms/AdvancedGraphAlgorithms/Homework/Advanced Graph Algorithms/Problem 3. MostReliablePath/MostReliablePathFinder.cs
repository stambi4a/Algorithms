namespace Problem_3.MostReliablePath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class MostReliablePathFinder
    {
        static void Main(string[] args)
        {
            int townsCount = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            string[] parameters = Console.ReadLine().Split();
            int startTown = int.Parse(parameters[1]);
            int endTown = int.Parse(parameters[3]);
            int edgesCount = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            double[,] graph = new double[townsCount, townsCount];
            for (int i = 0; i < townsCount; i++)
            {
                for (int j = 0; j < townsCount; j++)
                {
                    graph[i, j] = double.NegativeInfinity;
                }
            }

            int indexTowns = 1;
            while (indexTowns <= edgesCount)
            {
                parameters = Console.ReadLine().Split();
                int startNode = int.Parse(parameters[0]);
                int endNode = int.Parse(parameters[1]);
                double reliability = double.Parse(parameters[2]) / 100d;
                graph[startNode, endNode] = reliability;
                graph[endNode, startNode] = reliability;
                indexTowns++;
            }

            FindAndPrintShortestPath(graph, startTown, endTown);

        }

        static List<int> Dijkstra(double[,] graph, int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);

            double[] reliability = new double[n];
            for (int i = 0; i < n; i++)
            {
                reliability[i] = double.NegativeInfinity;
            }

            reliability[sourceNode] = 1;

            var used = new bool[n];
            int?[] previous = new int?[n];
            while (true)
            {
                double maxReliability = double.NegativeInfinity;
                int minNode = 0;
                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && reliability[node] > maxReliability)
                    {
                        maxReliability = reliability[node];
                        minNode = node;
                    }
                }
                if (double.IsNegativeInfinity(maxReliability))
                {
                    break;
                }

                used[minNode] = true;

                for (int i = 0; i < n; i++)
                {
                    if (graph[minNode, i] > double.NegativeInfinity)
                    {
                        double newDistance = reliability[minNode] * graph[minNode, i];
                        if (newDistance > reliability[i])
                        {
                            reliability[i] = newDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }

            if (double.IsNegativeInfinity(reliability[destinationNode]))
            {
                return null;
            }

            var path = new List<int>();
            int? currentNode = destinationNode;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            path.Reverse();
            return path;
        }

        static void FindAndPrintShortestPath(
        double[,] graph, int sourceNode, int destinationNode)
        {
            var path = Dijkstra(graph, sourceNode, destinationNode);
            if (path == null)
            {
                Console.WriteLine("no path");
            }
            else
            {
                double pathLength = 1.0d;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    pathLength *= graph[path[i], path[i + 1]];
                }
                var formattedPath = string.Join("->", path);
                Console.WriteLine("Most reliable path reliability: {0:p2}\n{1}", pathLength, formattedPath);
            }
        }
    }
}
