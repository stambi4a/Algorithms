namespace AreasInMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class AreasInMatrix
    {
        private static Dictionary<char, int> neighbourAreas;
        private static char[][] matrix;
        private static bool[,] visited;

        private static void Main(string[] args)
        {
            matrix = InputMatrix();
            visited = new bool[matrix.Length, matrix[0].Length];
            neighbourAreas = new Dictionary<char, int>();
            FindNeighbourAreas();
            PrintAreasCount();
        }

        private static void FindNeighbourAreas()
        {
            int rows = matrix.Length;
            int columns = matrix[0].Length;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (!visited[i, j])
                    {
                        CheckCell(i, j, rows, columns);
                        if(!neighbourAreas.ContainsKey(matrix[i][j]))
                        {
                            neighbourAreas.Add(matrix[i][j], 0);
                        }

                        neighbourAreas[matrix[i][j]]++;
                    }
                }
            }
        }

        private static void CheckCell(int row, int column, int rows, int columns)
        {
            visited[row, column] = true;
            if (column - 1 >= 0 && !visited[row, column - 1] && matrix[row][column - 1] == matrix[row][column])
            {
                CheckCell(row, column - 1, rows, columns);
            }

            if (column + 1 < columns && !visited[row, column + 1] && matrix[row][column + 1] == matrix[row][column])
            {
                CheckCell(row, column + 1, rows, columns);
            }

            if (row - 1 >= 0 && !visited[row - 1, column] && matrix[row - 1][column] == matrix[row][column])
            {
                CheckCell(row - 1, column, rows, columns);
            }

            if (row + 1 < rows && !visited[row + 1, column] && matrix[row + 1][column] == matrix[row][column])
            {
                CheckCell(row + 1, column, rows, columns);
            }
        }

        private static char[][] InputMatrix()
        {
            int countLines = int.Parse(Regex.Match(Console.ReadLine(), "\\d+").ToString());
            char[][] matrix = new char[countLines][];
            for (int i = 0; i < countLines; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }

            return matrix;
        }

        private static void PrintAreasCount()
        {
            int areasCount = neighbourAreas.Values.Sum();
            Console.WriteLine($"Areas: {areasCount}");
            neighbourAreas.Keys.ToList().ForEach(
                x =>
                    {
                        Console.WriteLine($"Letter '{x}' -> {neighbourAreas[x]}");
                    });
        }
    }
}
