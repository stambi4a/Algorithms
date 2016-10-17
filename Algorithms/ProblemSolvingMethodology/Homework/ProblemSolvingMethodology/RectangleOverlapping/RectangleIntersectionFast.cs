namespace RectangleOverlapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class RectangleIntersectionSolutionFast
    {
        private static void Main()
        {
            int rectangleCount = int.Parse(Console.ReadLine());
            var rectangles = new List<int[]>();
            List<int> xCoords = new List<int>(2 * rectangleCount);
            for (int i = 1; i <= rectangleCount; i++)
            {
                int[] rectangle = Console.ReadLine().Split().Select(int.Parse).ToArray();
                rectangles.Add(rectangle);
                xCoords.Add(rectangle[0]);
                xCoords.Add(rectangle[1]);
            }

            xCoords.Sort();

            for (int i = 0; i < 2 * rectangleCount; i++)
            {
                var overlappingRects =
                    rectangles.Where(
                        rectangle => rectangle[0] <= xCoords[2 * i + 1] && rectangle[1] >= xCoords[2 * i])
                        .ToList();
                int iterCount = overlappingRects.Count;
                var yCoords = new List<int>(2 * iterCount);
                for (int j = 0; j < iterCount; j++)
                {
                    yCoords.Add(overlappingRects[j][2]);
                    yCoords.Add(overlappingRects[j][3]);
                }

                var overlappingRectangles = overlappingRects.Where(rectangle => rectangle[2] <= yCoords[2 * i + 1] && rectangle[3] >= yCoords[2 * i])
                        .ToList();
            }
        }
    }
}