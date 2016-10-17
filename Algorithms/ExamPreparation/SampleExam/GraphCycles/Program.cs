namespace GraphCycles
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static SortedDictionary<int, SortedSet<int>> graph;
        private static Dictionary<int, int> nodesPredecessors;

        private static void Main(string[] args)
        {
            graph = new SortedDictionary<int, SortedSet<int>>();
            nodesPredecessors = new Dictionary<int, int>();
            ReadGraph();
            FindPredecessors();
            RemoveAllNonCycleNodes();
            var cycles = FindAllNonRepeatingCyclesOfLength3();
            if (cycles.Count == 0)
            {
                Console.WriteLine("No cycles found");
            }

            foreach (var cycle in cycles)
            {
                Console.WriteLine("{" + string.Join(" -> ", cycle) + "}");
            }
        }

        private static List<int[]> FindAllNonRepeatingCyclesOfLength3()
        {
            var cycles = new List<int[]>();
            while (true)
            {
                var isRemoved = false;
                var nodeToRemove = -1;
                foreach (var node in graph.Keys)
                {
                    foreach (var child in graph[node])
                    {
                        if (child == node)
                        {
                            continue;
                        }
                        foreach (var grandChild in graph[child])
                        {
                            if (grandChild == node || grandChild == child)
                            {
                                continue;
                            }

                            foreach (var grandGrandChild in graph[grandChild])
                            {
                                if (grandGrandChild == node)
                                {
                                    cycles.Add(new [] { node, child, grandChild });
                                    nodeToRemove = node;
                                    isRemoved = true;
                                }
                            }                          
                        }
                    }

                    if (nodeToRemove != -1)
                    {
                        break;
                    }
                }

                if (!isRemoved)
                {
                    break;
                }

                graph.Remove(nodeToRemove);
                foreach (var parent in graph.Keys)
                {
                    if (graph[parent].Contains(nodeToRemove))
                    {
                        graph[parent].Remove(nodeToRemove);
                    }
                }

                nodesPredecessors.Remove(nodeToRemove);


                RemoveAllNonCycleNodes();
                if (graph.Count <= 2)
                {
                    break;
                }
            }

            return cycles;
        }

        private static void RemoveAllNonCycleNodes()
        {
            while (true)
            {
                int nodeToRemove = -1;
                foreach (var node in graph.Keys)
                {
                    if (nodesPredecessors[node] == 0)
                    {
                        nodeToRemove = node;
                        foreach (var child in graph[node])
                        {
                            nodesPredecessors[child]--;
                        }

                        break;
                    }
                }

                if (nodeToRemove == -1)
                {
                    break;
                }

                graph.Remove(nodeToRemove);
                nodesPredecessors.Remove(nodeToRemove);
                foreach (var node in graph.Keys)
                {
                    if (graph[node].Contains(nodeToRemove))
                    {
                        graph[node].Remove(nodeToRemove);
                    }
                }
            }

            var nodesWithoutChilds = new List<int>();
            foreach (var node in nodesPredecessors.Keys)
            {
                if (graph[node].Count == 0)
                {
                    graph.Remove(node);
                    foreach (var parent in graph.Keys)
                    {
                        if (graph[parent].Contains(node))
                        {
                            graph[parent].Remove(node);
                        }
                    }

                    nodesWithoutChilds.Add(node);
                }
            }

            foreach (var node in nodesWithoutChilds)
            {
                nodesPredecessors.Remove(node);
            }      
        }

        private static void FindPredecessors()
        {
            foreach (var parent in graph.Keys)
            {
                if (!nodesPredecessors.ContainsKey(parent))
                {
                    nodesPredecessors.Add(parent, 0);
                }

                foreach (var child in graph[parent])
                {
                    if (!nodesPredecessors.ContainsKey(child))
                    {
                        nodesPredecessors.Add(child, 0);
                    }

                    nodesPredecessors[child]++;
                }               
            }
        }

        private static void ReadGraph()
        {
            int edgesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < edgesCount; i++)
            {
                string[] input = Console.ReadLine().Split();
                int parent = int.Parse(input[0]);
                if (!graph.ContainsKey(parent))
                {
                    graph.Add(parent, new SortedSet<int>());
                }

                int length = input.Length;
                for (int j = 2; j < length; j++)
                {
                    int child = int.Parse(input[j]);
                    if (!graph.ContainsKey(child))
                    {
                        graph.Add(child, new SortedSet<int>());
                    }

                    graph[parent].Add(child);
                }
            }
        }
    }
}
