namespace CyclesInGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class CyclesInGraph
    {
        private static Dictionary<char, HashSet<char>> graph;
        private static Dictionary<char, int> predecessorCount; 

        private static void Main(string[] args)
        {
            graph = new Dictionary<char, HashSet<char>>();
            predecessorCount = new Dictionary<char, int>();
            ReadGraph();
            SourceRemovalTopSorting();
        }

        private static void ReadGraph()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                char[] connectedNodes = input
                                        .Split(new[] { ' ', '-', }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(char.Parse)
                                        .ToArray();
                if (!graph.ContainsKey(connectedNodes[0]))
                {
                    graph.Add(connectedNodes[0], new HashSet<char>());
                }

                if (!graph[connectedNodes[0]].Contains(connectedNodes[1]))
                {
                    graph[connectedNodes[0]].Add(connectedNodes[1]);
                }

                if (!predecessorCount.ContainsKey(connectedNodes[0]))
                {
                    predecessorCount.Add(connectedNodes[0], 0);
                }

                if (!predecessorCount.ContainsKey(connectedNodes[1]))
                {
                    predecessorCount.Add(connectedNodes[1], 0);
                }

                predecessorCount[connectedNodes[1]]++;
            }
        }

        private static void SourceRemovalTopSorting()
        {
            bool nodeRemoved = true;
            while (nodeRemoved)
            {
                char topNode = ' ';
                nodeRemoved = false;
                foreach (var node in predecessorCount.Keys)
                {
                    if (predecessorCount[node] == 0)
                    {
                        nodeRemoved = true;
                        topNode = node;
                        if (graph.ContainsKey(node))
                        {
                            foreach (var child in graph[node])
                            {
                                predecessorCount[child]--;
                            }
                        }
                       

                        break;
                    }
                }

                if (topNode != ' ')
                {
                    graph.Remove(topNode);
                    predecessorCount.Remove(topNode);
                }
            }

            Console.Write("Acyclic: ");
            if (graph.Count > 0)
            {
                Console.WriteLine("No");
            }
            else
            {
                Console.WriteLine("Yes");
            }
        }
    }
}
