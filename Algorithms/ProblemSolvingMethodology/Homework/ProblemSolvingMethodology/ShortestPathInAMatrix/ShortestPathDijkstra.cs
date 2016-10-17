/*
namespace ShortestPathInAMatrix
{
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(
            Dictionary<Node, Dictionary<Node, int>> graph, 
            Dictionary<int, Node> nodes,
            int[] distances,
            int sourceNode, 
            int destinationNode)
        {
            int[] previous = new int[graph.Count];
            bool[] visited = new bool[graph.Count];
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            var startNode = nodes[sourceNode];
            startNode.DistanceFromStart = 0;

            for (int i = 0; i < previous.Length; i++)
            {
                previous[i] = -1;
            }

            priorityQueue.Enqueue(startNode);

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.ExtractMin();

                if (currentNode.Index == destinationNode)
                {
                    break;
                }

                foreach (var edge in graph[currentNode])
                {
                    if (!visited[edge.Key.Index])
                    {
                        priorityQueue.Enqueue(edge.Key);
                        visited[edge.Key.Index] = true;
                    }

                    var distance = currentNode.DistanceFromStart + edge.Value;
                    if (distance < edge.Key.DistanceFromStart)
                    {
                        edge.Key.DistanceFromStart = distance;
                        previous[edge.Key.Index] = currentNode.Index;
                        priorityQueue.DecreaseKey(edge.Key);
                    }
                }
            }

            if (previous[destinationNode] == -1)
            {
                return null;
            }

            List<int> path = new List<int>();
            int current = destinationNode;
            while (current != -1)
            {
                path.Add(distances[current]);
                current = previous[current];
            }

            path.Reverse();
            return path;
        }
    }
}
*/
