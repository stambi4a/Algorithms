namespace Problem_2.Modified_Kruskal_Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ModifiedKruskalAlgorithmMain
    {
        static List<Edge> edges;
        static void Main(string[] args)
        {
            edges = new List<Edge>();
            int edgesCount = int.Parse(Console.ReadLine().Split().ToArray()[1]);
            Dictionary<int, Node> nodes = new Dictionary<int, Node>();

            int edgeIndex = 1;
            while (edgeIndex <= edgesCount)
            {
                string[] parameters = Console.ReadLine().Split();
                Node startNode = null;
                int value = int.Parse(parameters[0]);
                if (!nodes.ContainsKey(value))
                {
                    startNode = new Node(value);
                    nodes.Add(value, startNode);
                }
                else
                {
                    startNode = nodes[value];
                }

                Node endNode = null;
                value = int.Parse(parameters[1]);
                if (!nodes.ContainsKey(value))
                {
                    endNode = new Node(value);
                    nodes.Add(value, endNode);
                }
                else
                {
                    endNode = nodes[value];
                }

                int distance = int.Parse(parameters[2]);
                Edge edge = new Edge(startNode, endNode, distance);
                edges.Add(edge);
                edgeIndex++;
            }

            var minimumSpanningForest = Kruskal(edges);

            Console.WriteLine("Minimum spanning forest weight: " +
                minimumSpanningForest.Sum(e => e.Weight));
            foreach (var edge in minimumSpanningForest)
            {
                Console.WriteLine(edge);
            }
        }

        static List<Edge> Kruskal(List<Edge> edges)
        {
            edges.Sort();

            var spanningTree = new List<Edge>();
            foreach (var edge in edges)
            {
                Node rootStartNode = FindRoot(edge.StartNode);
                Node rootEndNode = FindRoot(edge.EndNode);
                if (!rootStartNode.Equals(rootEndNode))
                {
                    spanningTree.Add(edge);
                    rootEndNode.Children.Add(rootStartNode);
                    rootStartNode.Parent = rootEndNode;
                    ChangeParent(rootStartNode, rootEndNode);
                }
            }

            return spanningTree;
        }

        static void ChangeParent(Node node, Node root)
        {
            if (node.Children.Count == 0)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                child.Parent = root;
                ChangeParent(child, root);
            }
        }

        static Node FindRoot(Node node)
        {
            return node.Parent;
        }
    }
}
