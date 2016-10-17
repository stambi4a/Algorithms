/*
namespace RectangleOverlapping
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class RectangleIntersectionSolutionFast
    {
        private static void Main()
        {
            int rectangleCount = int.Parse(Console.ReadLine());
            var rectangles = new List<int[]>();
            for (int i = 1; i <= rectangleCount; i++)
            {
                int[] rectangle = Console.ReadLine().Split().Select(int.Parse).ToArray();
                rectangles.Add(rectangle);
            }

            rectangles = rectangles.OrderBy(x => x[0]).ToList();
            var overlappingRectangles = FindOverlappingRectangles(rectangles).OrderBy(x => x[0]).ToList();
            var doubleOrMoreOverlappingRectangles = FindOverlappingRectangles(overlappingRectangles);
            var distinctRectangles = doubleOrMoreOverlappingRectangles.Distinct(new RectangleComparer());
            int areaRepeatedOverlapping = SumOverlappingRectangles(distinctRectangles);
            int areaOverlapping = SumOverlappingRectangles(overlappingRectangles);
            int areaDoubleOrMoreOverlapping = SumOverlappingRectangles(doubleOrMoreOverlappingRectangles);
            int overlappingArea = areaOverlapping - areaDoubleOrMoreOverlapping + areaRepeatedOverlapping;
            Console.WriteLine(overlappingArea);
        }

        private static List<int[]> FindOverlappingRectangles(List<int[]> rectangles)
        {
            int rectangleCount = rectangles.Count;
            var overlappingRectangles = new List<int[]>();
            for (int i = 0; i < rectangleCount - 1; i++)
            {
                for (int j = i + 1; j < rectangleCount; j++)
                {
                    if (rectangles[j][0] >= rectangles[i][1])
                    {
                        break;
                    }

                    if (rectangles[i][3] > rectangles[j][2])
                    {
                        int lower = rectangles[i][2] > rectangles[j][2] ? rectangles[i][2] : rectangles[j][2];
                        int upper = rectangles[i][3] < rectangles[j][3] ? rectangles[i][3] : rectangles[j][3];
                        int right = rectangles[i][1] < rectangles[j][1] ? rectangles[i][1] : rectangles[j][1];
                        int[] overlapping = { rectangles[j][0], right, lower, upper };
                        overlappingRectangles.Add(overlapping);
                    }
                }
            }

            return overlappingRectangles;
        }



        private static int SumOverlappingRectangles(IEnumerable<int[]> rectangles)
        {
            int sum = 0;
            foreach (var rectangle in rectangles)
            {
                int area = (rectangle[1] - rectangle[0]) * (rectangle[3] - rectangle[2]);
                sum += area;
            }

            return sum;
        }
    }

    internal class RectangleComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] int1, int[] int2)
        {
            if (int1[0] == int2[0] && int1[1] == int2[1] && int1[2] == int2[2] && int1[3] == int2[3])
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(int[] interval)
        {
            return 1;
        }
    }
}

/* internal class Rectangle : IComparable<Rectangle>
    {
        public Rectangle(int minX, int maxX ,int  minY ,int maxY )
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }

        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }

        public int CompareTo(Rectangle other)
        {
            return this.MinX - other.MinX;
        }
    }#1#

*/
