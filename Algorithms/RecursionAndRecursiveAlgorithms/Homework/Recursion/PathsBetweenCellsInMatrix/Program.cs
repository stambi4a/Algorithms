namespace PathsBetweenCellsInMatrix
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static char[,] matrix =
            {
                { 's', ' ', ' ', ' ' }, 
                { ' ', '*', '*', ' ' }, 
                { ' ', '*', '*', ' ' },
                { ' ', '*', 'e', ' ' }, 
                { ' ', ' ', ' ', ' ' },
            };

        private static List<Tuple<int, int>> path; 

        static void Main(string[] args)
        {
            path = new List<Tuple<int, int>>();
            FindPath(0, 0);
        }

        private static void PrintPath(int finalRow, int finalColumn)
        {
            Console.Write("Found the exit: ");
            foreach (var cell in path)
            {
                Console.Write("({0},{1}) -> ", cell.Item1, cell.Item2);
            }
            Console.WriteLine("({0},{1})", finalRow, finalColumn);
            Console.WriteLine();
        }

        private static void FindPath(int row, int col)
        {
            if (!InRange(row, col))
            {
                return;
            }

            if (matrix[row, col] == 'e')
            {
                PrintPath(row, col);
            }

            if (matrix[row, col] != ' ' && matrix[row, col] != 's')
            {
                // The current cell is not free -> can't find a path
                return;
            }

            matrix[row, col] = '*';
            path.Add(new Tuple<int, int>(row, col));
            FindPath(row + 1, col);
            FindPath(row, col - 1);
            FindPath(row - 1, col);
            FindPath(row, col + 1);

            matrix[row, col] = ' ';
            path.RemoveAt(path.Count - 1);

        }

        private static bool InRange(int row, int col)
        {
            bool rowInRange = row >= 0 && row < matrix.GetLength(0);
            bool colInRange = col >= 0 && col < matrix.GetLength(1);
            return rowInRange && colInRange;
        }
    }
}
