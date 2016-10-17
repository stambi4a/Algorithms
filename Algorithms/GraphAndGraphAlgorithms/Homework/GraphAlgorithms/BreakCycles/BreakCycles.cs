namespace BreakCycles
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    using Wintellect.PowerCollections;

    internal class BreakCycles
    {
        private const string EdgesToRemove = "Edges to remove: {0}";
        private static Dictionary<char, Bag<char>> graph;
        private static OrderedBag<string> connections;
        private static int edgesToRemove = 0;
        private static List<string> removedEdges; 
        private static HashSet<char> visitedNodes;

        private static void Main(string[] args)
        {
            graph = new Dictionary<char, Bag<char>>();
            connections = new OrderedBag<string>();
            removedEdges = new List<string>();
            ReadGraph();
            RemoveEdges();
            PrintRemovedEdges();
        }

        private static void PrintRemovedEdges()
        {
            Console.WriteLine(EdgesToRemove, edgesToRemove);
            Console.WriteLine(string.Join("\n", removedEdges));
        }

        private static void RemoveEdges()
        {
            foreach (var edge in connections)
            {
                char start = edge[0];
                char end = edge[1];
                graph[start].Remove(end);
                graph[end].Remove(start);
                visitedNodes = new HashSet<char>();
                bool pathIsFound = DfsFindPathBetweenTwoNodes(start, end);
                if (!pathIsFound)
                {
                    graph[start].Add(end);
                    graph[end].Add(start);
                }
                else
                {
                    edgesToRemove++;
                    removedEdges.Add($"{edge[0]} - {edge[1]}");
                }
            }
        }

        private static bool DfsFindPathBetweenTwoNodes(char start, char end)
        {
            if (start == end)
            {
                return true;
            }

            foreach (var child in graph[start])
            {
                if (!visitedNodes.Contains(child))
                {
                    visitedNodes.Add(child);
                    if(DfsFindPathBetweenTwoNodes(child, end))
                    {
                        return true;
                    }
                }
            }

            return false;
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

                char[] nodes =
                    input.Split(new[] { '-', '>', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(char.Parse)
                        .ToArray();

                int length = nodes.Length;
                if (!graph.ContainsKey(nodes[0]))
                {
                    graph.Add(nodes[0], new Bag<char>());
                }

                for (int i = 1; i < length; i++)
                {
                    graph[nodes[0]].Add(nodes[i]);
                    if (nodes[0] < nodes[i])
                    {
                        string connection = new string(new[] { nodes[0], nodes[i] });
                        connections.Add(connection);
                    }                    
                }
            }
        }
    }
}
