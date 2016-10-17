namespace Problem_2.Modified_Kruskal_Algorithm
{
    using System;

    public class Edge : IComparable<Edge>
    {
        public Edge(Node startNode, Node endNode, int weight)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;
        }

        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public int Weight { get; set; }

        public int CompareTo(Edge other)
        {
            int weightCompared = this.Weight.CompareTo(other.Weight);
            return weightCompared;
        }

        public override string ToString()
        {
            return string.Format("({0} {1}) -> {2}",
                this.StartNode, this.EndNode, this.Weight);
        }
    }
}
