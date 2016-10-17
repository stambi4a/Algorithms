namespace DistanceBetweenVertices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class DistanceBetweenVertices
    {
        private static Dictionary<int, HashSet<int>> graph; //contains vertices with their childs

        private static Dictionary<Tuple<int, int>, int> distances; //contains already checked distances

        private static void Main(string[] args)
        {
            ReadGraph();
            CheckDistances();
        }

        //Reads input from the console in the format given in the homework input.
        //Creates graph from the input
        private static void ReadGraph()
        {
            List<string> graphInput = new List<string>();
            Console.ReadLine();
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "Distances to find:")
                {
                    break;
                }

                graphInput.Add(line);
            }

            graph = new Dictionary<int, HashSet<int>>();
            foreach (var line in graphInput)
            {
                int[] nodes = line.Split(new [] { ' ', '-' , '>', ',' }, StringSplitOptions.RemoveEmptyEntries).
                    Select(int.Parse).
                    ToArray();
                int parent = nodes[0];
                if (!graph.ContainsKey(parent))
                {
                    graph.Add(parent, new HashSet<int>());
                }

                if (nodes.Length >= 2)
                {
                    for (int i = 1; i < nodes.Length; i++)
                    {
                        int child = nodes[i];
                        graph[parent].Add(child);
                    }
                    
                }                
            }
        }

        //
        private static void CheckDistances()
        {
            distances = new Dictionary<Tuple<int, int>, int>();
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                int[] nodes = line.Split(new[] { '-' }).
                    Select(int.Parse).
                    ToArray();
                int parent = nodes[0];
                int child = nodes[1];
                int distance = FindDistance(parent, child);
                Console.WriteLine($"{{{parent}, {child}}} -> " + distance);
            }            
        }

        //Reads input from the console in format given in the homework description examples
        //Extract pair start-end for distance search
        private static int FindDistance(int start, int end)
        {
            Queue<int> nodes = new Queue<int>(); // use queue for BFS algorithm
            Dictionary<int, bool> visited = new Dictionary<int, bool>(); // visited nodes in currentSearch
            foreach (var node in graph.Keys)
            {
                if(!visited.ContainsKey(node))
                {
                    visited.Add(node, false);
                }
            }

            if (start == end) // check if start coincide with end
            {
                return 0;
            }

            Tuple<int, int> startEnd = new Tuple<int, int>(start, end);
            if (distances.ContainsKey(startEnd)) // check if this search is already made
            {
                return distances[startEnd];
            }

            int distance = 1;
            int parent = start;
            nodes.Enqueue(parent);
            visited[parent] = true;
            int oldCount = 1; // Count of nodes same distance from start, no more nodes are added to this count
            int newCount = 0; // Count of nodes same distance from start, new nodes increase this count
            while (true)
            {
                Tuple<int, int> parentEnd = new Tuple<int, int>(parent, end);
                if (distances.ContainsKey(parentEnd)) // Check if search for current pair parent - end is already made
                {
                    distance = distance - 1 + distances[parentEnd];
                    distances.Add(startEnd, distance);
                    return distance;
                }

                if (graph[parent].Contains(end)) // Checks for end variable in children, if parent has any
                {
                    distances.Add(startEnd, distance);
                    return distance;
                }

                else
                {
                    foreach (var node in graph[parent])
                    {
                        if (!visited[node]) // Avoid cycling
                        {
                            visited[node] = true;
                            nodes.Enqueue(node);
                            newCount++;
                        }
                    }
                }

                nodes.Dequeue();
                oldCount--;
                if (nodes.Count == 0) // All nodes are exhausted and method returns -1
                {
                    distances.Add(startEnd, -1); 
                    return -1;
                }

                if (oldCount == 0) // No more nodes at current distance from start, so search continues with nodes at distance + 1 from start
                {
                    oldCount = newCount;
                    newCount = 0;
                    distance++;
                }
                
                parent = nodes.Peek();
            }
        }
    }
}
