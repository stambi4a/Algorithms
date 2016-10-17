namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode)
        {
            int[] previous = new int[graph.Count];
            bool[] visited = new bool[graph.Count];
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            sourceNode.DistanceFromStart = 0;

            for (int i = 0; i < previous.Length; i++)
            {
                previous[i] = -1;
            }

            priorityQueue.Enqueue(sourceNode);

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.ExtractMin();

                if (currentNode.Id == destinationNode.Id)
                {
                    break;
                }

                foreach (var edge in graph[currentNode])
                {
                    if (!visited[edge.Key.Id])
                    {
                        priorityQueue.Enqueue(edge.Key);
                        visited[edge.Key.Id] = true;
                    }

                    var distance = currentNode.DistanceFromStart + edge.Value;
                    if (distance < edge.Key.DistanceFromStart)
                    {
                        edge.Key.DistanceFromStart = distance;
                        previous[edge.Key.Id] = currentNode.Id;
                        priorityQueue.DecreaseKey(edge.Key);
                    }
                }
            }

            if (previous[destinationNode.Id] == -1)
            {
                return null;
            }

            List<int> path = new List<int>();
            int current = destinationNode.Id;
            while (current != -1)
            {
                path.Add(current);
                current = previous[current];
            }

            path.Reverse();
            return path;
        }
    }
}
