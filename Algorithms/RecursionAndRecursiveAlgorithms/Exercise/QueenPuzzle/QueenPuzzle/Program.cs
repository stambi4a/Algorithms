namespace QueenPuzzle
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        const int Size = 8;

        static bool[,] chessBoard = new bool[Size, Size];
        private static int solutionsFind = 0;
        private static HashSet<int> attackedRows = new HashSet<int>();
        private static HashSet<int> attackedColumns = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();
        private static void Main(string[] args)
        {
            PutQueens(0);
        }

        private static void PutQueens(int row)
        {
            if (row == Size)
            {
                Console.WriteLine(++solutionsFind);
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueens(row + 1);
                        UnmarkAllAttackedPositions(row, col);
                    }
                }
                    
            }
                
        }

        private static void PrintSolution()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (chessBoard[i, j])
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            /*Console.ReadKey();*/
        }


        private static bool CanPlaceQueen(int row, int col)
        {
            if (attackedRows.Contains(row) ||
                attackedColumns.Contains(col) ||
                attackedLeftDiagonals.Contains(col - row) ||
                attackedRightDiagonals.Contains(row + col))
            {
                return false;
            }

            return true;
        }

        private static void MarkAllAttackedPositions(int row, int col)
        {
            attackedRows.Add(row);
            attackedColumns.Add(col);
            attackedLeftDiagonals.Add(col - row);
            attackedRightDiagonals.Add(row + col);
            chessBoard[row, col] = true;
        }

        private static void UnmarkAllAttackedPositions(int row, int col)
        {
            attackedRows.Remove(row);
            attackedColumns.Remove(col);
            attackedLeftDiagonals.Remove(col - row);
            attackedRightDiagonals.Remove(row + col);
            chessBoard[row, col] = false;
        }
    }
}
