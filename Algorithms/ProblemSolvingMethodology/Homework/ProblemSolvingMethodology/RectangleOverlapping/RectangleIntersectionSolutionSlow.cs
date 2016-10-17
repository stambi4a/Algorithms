/*
namespace RectangleOverlapping
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    internal class RectangleIntersectionSolutionSlow
    {
        private const int CoordinatesRadius = 1000;
        private static void Main(string[] args)
        {
            int size = 2 * CoordinatesRadius + 1;
            int[,] matrix = new int[size, size];
            int rectangles = int.Parse(Console.ReadLine());
            for (int i = 1; i <= rectangles; i++)
            {
                int[] coords = Console.ReadLine().Split().Select(x => int.Parse(x) + CoordinatesRadius).ToArray();
                for (int j = coords[0] + 1; j <= coords[1]; j++)
                {
                    for (int k = coords[2] + 1; k <= coords[3]; k++)
                    {
                        matrix[j, k]++;
                    }
                }
            }

            int countOverlaps = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] > 1)
                    {
                        countOverlaps++;
                    }
                }
            }

            Console.WriteLine(countOverlaps);
        }
    }
}
*/
