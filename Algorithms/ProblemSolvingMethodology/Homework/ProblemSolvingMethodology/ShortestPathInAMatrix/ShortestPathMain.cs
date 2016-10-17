namespace ShortestPathInAMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DijkstraMain
    {
        public static void Main()
        {
            var nodes = new Dictionary<int, Node>();
            int rows = int.Parse(Console.ReadLine());
            int columns = int.Parse(Console.ReadLine());
            int size = rows * columns;
            int[] distances = new int[size];
            for (int i = 0; i < rows; i++)
            {
                string[] input = Console.ReadLine().Split().ToArray();
                for (int j = 0; j < columns; j++)
                {
                    
                    int index = i * columns + j;
                    Node node = new Node(index);
                    nodes.Add(index, node);
                    distances[index] = int.Parse(input[j]);
                }
                
            }

            var graph = new Dictionary<Node, Dictionary<Node, int>>();
            for (int i = 0; i < size; i++)
            {
                Dictionary<Node,int> children = new Dictionary<Node, int>();
                if (i % columns != 0)
                {
                    children.Add(nodes[i - 1], distances[i - 1]);
                }

                if (i % columns != columns - 1)
                {
                    children.Add(nodes[i + 1], distances[i + 1]);
                }

                if (i / columns  > 0)
                {
                    children.Add(nodes[i - columns], distances[i - columns]);
                }

                if (i / columns < rows - 1)
                {
                    children.Add(nodes[i + columns], distances[i + columns]);
                }
                graph.Add(nodes[i], children);
            }

            PrintPath(graph, nodes, distances, 0, size - 1, distances[0]);
        }

        public static void PrintPath(
            Dictionary<Node, Dictionary<Node, int>> graph, 
            Dictionary<int, Node> nodes, 
            int[] distances,
            int sourceNode, 
            int destinationNode, 
            int distanceStartNode)
        {
            var path = DijkstraWithPriorityQueue.DijkstraAlgorithm(graph, nodes, distances, sourceNode, destinationNode);

            var formattedPath = string.Join(" ", path);
            Console.WriteLine("Length: {0}", nodes[destinationNode].DistanceFromStart + distanceStartNode);
            Console.WriteLine("Path: {0}", formattedPath);
        }
    }

    public class Node : IComparable<ShortestPathInAMatrix.Node>
    {
        public Node(int index, int distance = int.MaxValue)
        {
            this.Index = index;
            this.DistanceFromStart = distance;
        }

        public int Index { get; set; }

        public int DistanceFromStart { get; set; }

        public int CompareTo(ShortestPathInAMatrix.Node other)
        {
            return this.DistanceFromStart.CompareTo(other.DistanceFromStart);
        }

        public override string ToString()
        {
            return this.Index.ToString();
        }
    }

    public class Edge
    {
        public Edge(int parentNode, int childNode, int distance)
        {
            this.Parent = parentNode;
            this.Child = childNode;
            this.Distance = distance;
        }

        public int Distance { get; set; }

        public int Parent { get; set; }

        public int Child { get; set; }
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private Dictionary<T, int> searchCollection;
        private List<T> heap;

        public PriorityQueue()
        {
            this.heap = new List<T>();
            this.searchCollection = new Dictionary<T, int>();
        }

        public int Count
        {
            get
            {
                return this.heap.Count;
            }
        }

        public T ExtractMin()
        {
            var min = this.heap[0];
            this.heap[0] = this.heap[this.heap.Count - 1];
            this.heap.RemoveAt(this.heap.Count - 1);
            if (this.heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            this.searchCollection.Remove(min);

            return min;
        }

        public T PeekMin()
        {
            return this.heap[0];
        }

        public void Enqueue(T element)
        {
            this.searchCollection.Add(element, this.heap.Count);
            this.heap.Add(element);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyDown(int i)
        {
            var left = (2 * i) + 1;
            var right = (2 * i) + 2;
            var smallest = i;

            if (left < this.heap.Count && this.heap[left].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this.heap.Count && this.heap[right].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                T old = this.heap[i];
                this.searchCollection[old] = smallest;
                this.searchCollection[this.heap[smallest]] = i;
                this.heap[i] = this.heap[smallest];
                this.heap[smallest] = old;
                this.HeapifyDown(smallest);
            }
        }

        private void HeapifyUp(int i)
        {
            var parent = (i - 1) / 2;
            while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) < 0)
            {
                T old = this.heap[i];
                this.searchCollection[old] = parent;
                this.searchCollection[this.heap[parent]] = i;
                this.heap[i] = this.heap[parent];
                this.heap[parent] = old;

                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void DecreaseKey(T element)
        {
            int index = this.searchCollection[element];
            this.HeapifyUp(index);
        }
    }

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