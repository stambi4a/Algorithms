/*
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
            this.Children = new SortedSet<Rectangle>();
        }

        public void CheckRectangleIsNested(Rectangle other)
        {
            if (other.Left >= this.Left && other.Top <= this.Top && other.Right <= this.Right
                && other.Bottom >= this.Bottom)
            {
                this.Children.Add(other);
            }
        }

        public string Name { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public SortedSet<Rectangle> Children { get; set; }

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
*/
