namespace NestedRectangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static SortedDictionary<string, Rectangle> rectangles;
        private static Dictionary<string, Tuple<int, string>> maxNestedSequences;
        private static Tuple<int, string> maxNestedGivenRectangle;
        private static Tuple<int, string> maxNestedSequence;

        private static void Main(string[] args)
        {
            rectangles = new SortedDictionary<string, Rectangle>();
            maxNestedSequences = new Dictionary<string, Tuple<int, string>>();
            maxNestedSequence = new Tuple<int, string> (0, null);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                string[] parameters = input.Split(new char[] { ' ', ':' },
                StringSplitOptions.RemoveEmptyEntries);
                string name = parameters[0];
                /*string pattern = "[A-Za-z0-9]+";
                string match = Regex.Match(name, pattern).ToString(); */
                /*if (match.Length < name.Length || rectangles.ContainsKey(name))
                {
                    continue;
                }*/

                int left = int.Parse(parameters[1]);
                int top = int.Parse(parameters[2]);
                int right = int.Parse(parameters[3]);
                int bottom = int.Parse(parameters[4]);

                Rectangle rectangle = new Rectangle(name, left, top, right, bottom);
                rectangles.Add(name, rectangle);
            }


            FindChildren();
            FindDeepestNestedSequenceOfRectangles();
            Console.WriteLine(maxNestedSequence.Item2);
        }

        private static void FindDeepestNestedSequenceOfRectangles()
        {
            foreach (var rectangle in rectangles.Keys)
            {
                maxNestedGivenRectangle = new Tuple<int, string>(0, null);
                var currentNested = new List<string> {rectangle};
                maxNestedGivenRectangle = BfsDeepestNestedSequenceRectangles(currentNested, rectangle);
                
                maxNestedSequences.Add(rectangle, maxNestedGivenRectangle);
                if (maxNestedGivenRectangle.Item1 > maxNestedSequence.Item1)
                {                   
                    maxNestedSequence = maxNestedGivenRectangle;
                }
            }
        }

        private static Tuple<int, string> BfsDeepestNestedSequenceRectangles(
            List<string> currentNested, 
            string rectangle)
        {
            if (rectangles[rectangle].Children.Count == 0)
            {
                if (currentNested.Count > maxNestedGivenRectangle.Item1)
                {
                    StringBuilder nested = new StringBuilder();
                    int count = currentNested.Count;
                    for (int i = 0; i < count - 1; i++)
                    {
                        nested.Append(currentNested[i] + " < ");
                    }

                    nested.Append(currentNested[count - 1]);
                    maxNestedGivenRectangle = new Tuple<int, string>(count, nested.ToString());
                }

                return maxNestedGivenRectangle;
            }

            foreach (var child in rectangles[rectangle].Children.Keys)
            {
                if (maxNestedSequences.ContainsKey(child))
                {                  
                    int nestedCount = currentNested.Count + maxNestedSequences[child].Item1;
                    if (nestedCount > maxNestedGivenRectangle.Item1)
                    {
                        StringBuilder nested = new StringBuilder();
                        int count = currentNested.Count;
                        for (int i = 0; i < count; i++)
                        {
                            nested.Append(currentNested[i] + " < ");
                        }

                        nested.Append(maxNestedSequences[child].Item2);
                        maxNestedGivenRectangle = new Tuple<int, string>(nestedCount, nested.ToString());
                    }

                    
                }
                else
                {
                    currentNested.Add(child);
                    BfsDeepestNestedSequenceRectangles(currentNested, child);
                    currentNested.RemoveAt(currentNested.Count - 1);
                }
            }

            return maxNestedGivenRectangle;
   ;     }

        private static void FindChildren()
        {
            var rects = rectangles.Keys.ToList();
            for (int i = 0; i < rects.Count; i++)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    rectangles[rects[i]].CheckRectangleIsNested(rectangles[rects[j]]);
                }

                for (int j = i + 1; j < rectangles.Count; j++)
                {
                    rectangles[rects[i]].CheckRectangleIsNested(rectangles[rects[j]]);
                }
            }
        }
    }
}

namespace NestedRectangles
{
    using System;
    using System.Collections.Generic;

    internal class Rectangle : IComparable<Rectangle>
    {
        internal Rectangle(string name, int left, int top, int right, int bottom)
        {
            this.Name = name;
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Children = new SortedDictionary<string, Rectangle>();
        }

        public void CheckRectangleIsNested(Rectangle other)
        {
            if (other.Left >= this.Left && other.Top <= this.Top && other.Right <= this.Right
                && other.Bottom >= this.Bottom)
            {
                this.Children.Add(other.Name, other);
            }
        }

        public string Name { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public SortedDictionary<string, Rectangle> Children { get; set; }

        public int CompareTo(Rectangle other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

