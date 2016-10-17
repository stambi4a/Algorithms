namespace Snakes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.SymbolStore;

    internal class Snake
    {
        private static Dictionary<int, HashSet<int>> coordinates = new Dictionary<int, HashSet<int>>();
        private static void Main(string[] args)
        {
            char[] free = { 'r', 'd', 'l', 'u' };
            int n = 7;
            char[] arr = new char[n];
            arr[0] = 's';
            coordinates.Add(0, new HashSet<int>());
            coordinates[0].Add(0);
            CreateSnake(n, arr, 1, false, 'r', 0, 1);
        }

        private static void DrawSnake()
        {
            
        }

        private static void CreateSnake(int n, char[] arr, int index, bool dIsUsed, char direction, int row, int col)
        {            
            if (CheckIfCross(row, col))
            {
                return;
            }

            arr[index] = direction;
            if (!coordinates.ContainsKey(row))
            {
                coordinates.Add(row, new HashSet<int>());
            }

            coordinates[row].Add(col);
            if (index == n - 1)
            {
                Console.WriteLine(new string(arr));
                coordinates[row].Remove(col);
                return;
            }

            if (direction == 'r')
            {
                CreateSnake(n, arr, index + 1, dIsUsed, 'r', row, col + 1);
                CreateSnake(n, arr, index + 1, dIsUsed, 'd', row + 1, col);
                if (dIsUsed)
                {
                    CreateSnake(n, arr, index + 1, dIsUsed, 'u', row - 1, col);
                }

                coordinates[row].Remove(col);
            }

            if (direction == 'd')
            {
                dIsUsed = true;
                CreateSnake(n, arr, index + 1, dIsUsed, 'r', row, col + 1);
                CreateSnake(n, arr, index + 1, dIsUsed, 'l', row, col - 1);
                coordinates[row].Remove(col);
            }

            if (direction == 'u')
            {
                CreateSnake(n, arr, index + 1, dIsUsed, 'r', row, col + 1);
                CreateSnake(n, arr, index + 1, dIsUsed, 'l', row, col - 1);
                coordinates[row].Remove(col);
            }

            if (direction == 'l')
            {
                CreateSnake(n, arr, index + 1, dIsUsed, 'd', row + 1, col);
                CreateSnake(n, arr, index + 1, dIsUsed, 'u', row - 1, col);
                coordinates[row].Remove(col);
            }
        }

        private static bool CheckIfCross(int row, int col)
        {
            if (coordinates.ContainsKey(row) && coordinates[row].Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}
