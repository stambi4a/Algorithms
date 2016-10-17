namespace AStarAlgorithm
{
    using System;
    using System.Collections.Generic;

    public class AStar
    {
        private readonly char[,] map;
        private readonly PriorityQueue<Node> openNodesByFCost;
        private readonly HashSet<Node> closedSet;
        private readonly Node[,] graph;

        public AStar(char[,] map)
        {
            this.map = map;
            this.openNodesByFCost = new PriorityQueue<Node>();
            this.closedSet = new HashSet<Node>();
            this.graph = new Node[map.GetLength(0), map.GetLength(1)];
        }

        public List<int[]> FindShortestPath(int[] startCoords, int[] endCoords)
        {
            var startNode = this.GetNode(startCoords[0], startCoords[1]);
            startNode.GCost = 0;
            this.openNodesByFCost.Enqueue(startNode);
            while (this.openNodesByFCost.Count > 0)
            {
                var currentNode = this.openNodesByFCost.ExtractMin();
                this.closedSet.Add(currentNode);
                if (currentNode.Row == endCoords[0] && currentNode.Col == endCoords[1])
                {
                    return ReconstructPath(currentNode);
                }

                var neighbours = this.GetNeighbours(currentNode);
                foreach (var neighbour in neighbours)
                {
                    if (this.closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    var gCost = currentNode.GCost + CalculateGCost(neighbour, currentNode);
                    if (gCost < neighbour.GCost)
                    {
                        neighbour.GCost = gCost;
                        neighbour.Parent = currentNode;
                    }

                    if (!this.openNodesByFCost.Contains(neighbour))
                    {
                        neighbour.HCost = CalculateHCost(neighbour, endCoords);
                        this.openNodesByFCost.Enqueue(neighbour);
                    }
                    else
                    {
                        this.openNodesByFCost.DecreaseKey(neighbour);

                    }
                }
            }

            return new List<int[]>(0);
        }

        private static List<int[]> ReconstructPath(Node currentNode)
        {
            var cells = new List<int[]>();
            while (currentNode != null)
            {
                cells.Add(new[] { currentNode.Row, currentNode.Col });
                currentNode = currentNode.Parent;
            }

            return cells;
        }

        private static int CalculateHCost(Node node, int[] endCoords)
        {
            return GetDistance(node.Row, node.Col, endCoords[0], endCoords[1]);
        }

        private static int CalculateGCost(Node node, Node prev)
        {
            return GetDistance(node.Row, node.Col, prev.Row, prev.Col);
        }

        private static int GetDistance(int r1, int c1, int r2, int c2)
        {
            var deltaX = Math.Abs(c1 - c2);
            var deltaY = Math.Abs(r1 - r2);
            if (deltaX > deltaY)
            {
                return 14 * deltaY + 10 * (deltaX - deltaY);
            }

            return 14 * deltaX + 10 * (deltaY - deltaX);
        }

        private List<Node> GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();
            var maxRow = this.graph.GetLength(0);
            var maxColumn = this.graph.GetLength(1);
            for (int row = node.Row - 1; row < node.Row + 1 && row < maxRow; row++)
            {
                if (row < 0)
                {
                    continue;
                }

                for (int col = node.Col - 1; col <= node.Col + 1 && col < maxColumn; col++)
                {
                    if (this.map[row, col] != 'W' && this.graph[row, col] != node)
                    {
                        var neighbour = this.GetNode(row, col);
                        neighbours.Add(neighbour);
                    }
                }
            }

            return neighbours;
        }



        private Node GetNode(int row, int col)
        {
            return this.graph[row, col] ?? (this.graph[row, col] = new Node(row, col));
        }
    }
}
