namespace Problem_3.Knight_s_Tour
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class KnightsTour
    {
        private static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            bool[,] visited = new bool[size, size];
            int[,] knightMoves = new int[size, size];
            int row = 0;
            int column = 0;
            int maxMoves = knightMoves.Length;
            int currentMove = 1;
            while (currentMove <= maxMoves)
            {
                visited[row, column] = true;
                knightMoves[row, column] = currentMove;
                currentMove++;
                var possibleMoves = new List<int[]>();
                CheckPossibleFields(row, column, visited, possibleMoves);
                int minNextFieldPossibleMoves = maxMoves - 1;
                foreach (var field in possibleMoves)
                {
                    visited[field[0], field[1]] = true;
                    var nextFieldPossibleMoves = new List<int[]>();
                    CheckPossibleFields(field[0], field[1], visited, nextFieldPossibleMoves);
                    if (nextFieldPossibleMoves.Count < minNextFieldPossibleMoves)
                    {
                        minNextFieldPossibleMoves = nextFieldPossibleMoves.Count;
                        row = field[0];
                        column = field[1];
                    }

                    visited[field[0], field[1]] = false;
                }
            }

            PrintMoves(knightMoves);
        }

        private static void PrintMoves(int[,] knightMoves)
        {
            int size = knightMoves.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(knightMoves[i, j].ToString().PadRight(5));
                }

                Console.WriteLine();
            }
        }

        private static void CheckPossibleFields(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMoveRightUp(row, column, visited, possibleMoves);
            CheckMoveUpRight(row, column, visited, possibleMoves);
            CheckMoveUpLeft(row, column, visited, possibleMoves);
            CheckMoveLeftUp(row, column, visited, possibleMoves);
            CheckMoveLeftDown(row, column, visited, possibleMoves);
            CheckMoveRightDown(row, column, visited, possibleMoves);
            CheckMoveDownLeft(row, column, visited, possibleMoves);
            CheckMoveDownRight(row, column, visited, possibleMoves);
        }

        private static void CheckMoveRightUp(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row - 1, column + 2, visited, possibleMoves);
        }

        private static void CheckMoveUpRight(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row - 2, column + 1, visited, possibleMoves);
        }

        private static void CheckMoveUpLeft(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row - 2, column - 1, visited, possibleMoves);
        }

        private static void CheckMoveLeftUp(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row - 1, column - 2, visited, possibleMoves);
        }

        private static void CheckMoveLeftDown(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row + 1, column - 2, visited, possibleMoves);
        }

        private static void CheckMoveDownLeft(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row + 2, column - 1, visited, possibleMoves);
        }

        private static void CheckMoveDownRight(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row + 2, column + 1, visited, possibleMoves);
        }

        private static void CheckMoveRightDown(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            CheckMove(row + 1, column + 2, visited, possibleMoves);
        }


        private static void CheckMove(int row, int column, bool[,] visited, List<int[]> possibleMoves)
        {
            int size = visited.GetLength(0);
            if (row < 0 || row >= size || column < 0 || column >= size || visited[row, column])
            {
                return;
            }

            possibleMoves.Add(new[] { row, column });
        }
    }
}
