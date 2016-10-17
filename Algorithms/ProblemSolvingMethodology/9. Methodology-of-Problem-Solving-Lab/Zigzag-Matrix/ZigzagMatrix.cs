namespace Zigzag_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ZigzagMatrix
    {
        public static void Main(string[] args)
        {
            int numberOfRows = int.Parse(Console.ReadLine());
            int numberOfColumns = int.Parse(Console.ReadLine());

            int[][] matrix = new int[numberOfRows][];
            ReadMatrix(numberOfRows, matrix);

            int[,] maxPaths = new int[numberOfRows, numberOfColumns];
            int[,] previousRowIndex = new int[numberOfRows, numberOfColumns];

            for (int i = 1; i < numberOfRows; i++)
            {
                maxPaths[i, 0] = matrix[i][0];
            }

            for (int i = 1; i < numberOfColumns; i++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    int previousMax = 0;
                    if (i % 2 == 1)
                    {
                        for (int j = row + 1; j < numberOfRows; j++)
                        {
                            if (maxPaths[j, i - 1] > previousMax)
                            {
                                previousMax = maxPaths[j, i - 1];
                                previousRowIndex[row, i] = j;
                            }
                        }
                    }
                    else
                    {
                        for (int j = row - 1; j >= 0; j--)
                        {
                            if (maxPaths[j, i - 1] > previousMax)
                            {
                                previousMax = maxPaths[j, i - 1];
                                previousRowIndex[row, i] = j;
                            }
                        }
                    }

                    maxPaths[row, i] = previousMax + matrix[row][i];
                }
            }

            var currentRowIndex = GetLastRowIndexOfPath(maxPaths, numberOfColumns);
            var path = RecoverMaxPath(numberOfColumns, matrix, currentRowIndex, previousRowIndex);
            Console.WriteLine($"{maxPaths[currentRowIndex,numberOfColumns - 1]} = {string.Join(" + ", path)}");
        }

        private static void ReadMatrix(int numberOfRows, int[][] matrix)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }
        }

        private static int GetLastRowIndexOfPath(int[,] maxPaths, int numberOfColumns)
        {
            int currentRowIndex = -1;
            int globalMax = 0;
            for (int i = 0; i < maxPaths.GetLength(0); i++)
            {
                if (maxPaths[i, numberOfColumns - 1] > globalMax)
                {
                    globalMax = maxPaths[i, numberOfColumns - 1];
                    currentRowIndex = i;
                }
            }

            return currentRowIndex;
        }

        private static List<int> RecoverMaxPath(
            int numberOfColumns, 
            int[][] matrix, 
            int rowIndex, 
            int[,] previousRowIndex)
        {
            List<int> path = new List<int>();
            int columnIndex = numberOfColumns - 1;
            while (columnIndex >= 0)
            {
                path.Add(matrix[rowIndex][columnIndex]);
                rowIndex = previousRowIndex[rowIndex, columnIndex];
                columnIndex--;
            }

            path.Reverse();

            return path;
        }
    }
}