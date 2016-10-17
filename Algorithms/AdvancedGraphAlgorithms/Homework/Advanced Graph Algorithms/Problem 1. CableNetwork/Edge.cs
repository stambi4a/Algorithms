using System;

public class Edge : IComparable<Edge>
{
    public Edge(int startNode, int endNode, int cost)
    {
        this.StartNode = startNode;
        this.EndNode = endNode;
        this.Cost = cost;
    }

    public int StartNode { get; set; }
    public int EndNode { get; set; }
    public int Cost { get; set; }

    public int CompareTo(Edge other)
    {
        int weightCompared = this.Cost.CompareTo(other.Cost);
        return weightCompared;
    }

    public override string ToString()
    {
        return string.Format("({0} {1}) -> {2}",
            this.StartNode, this.EndNode, this.Cost);
    }
}
