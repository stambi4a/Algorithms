namespace Problem_2.Modified_Kruskal_Algorithm
{
    using System.Collections.Generic;

    public class Node
    {
        public Node(int value)
        {
            this.Value = value;
            this.Children = new HashSet<Node>();
            this.Parent = this;
        }

        public int Value { get; set; }
        public Node Parent { get; set; }
        public HashSet<Node> Children { get; set; }

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}
