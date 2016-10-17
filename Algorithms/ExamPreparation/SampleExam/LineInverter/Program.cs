namespace LineInverter
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            char[,] matrix = new char[size, size];
            char[,] copyMatrix = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                string input = Console.ReadLine();
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = input[j];
                    copyMatrix[i, j] = input[j];
                }
            }

            

            int countMinimumNumberOfInversionsFirstWay = LineInverterFirstWay(matrix);

            bool matrixIsBlack = CheckIfMatrixIsblack(matrix);
            if (!matrixIsBlack)
            {
                Console.WriteLine("-1");
                return;
            }

            int countMinimumNumberOfInversionsSecondWay = LineInverterSecondWay(copyMatrix);
            int countMinimumNumberOfInversions = countMinimumNumberOfInversionsFirstWay
                                                 < countMinimumNumberOfInversionsSecondWay
                                                     ? countMinimumNumberOfInversionsFirstWay
                                                     : countMinimumNumberOfInversionsSecondWay;
            Console.WriteLine(countMinimumNumberOfInversions);
        }

        private static bool CheckIfMatrixIsblack(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 'W')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int LineInverterSecondWay(char[,] copy)
        {
            int size = copy.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                copy[i, 0] = copy[i, 0] == 'W' ? 'B' : 'W';
            }

            int countMinimumNumberOfInversions = LineInverterFirstWay(copy) + 1;

            return countMinimumNumberOfInversions;
        }

        private static int LineInverterFirstWay(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            int countMinimumNumberOfInversions = 0;

            for (int i = 0; i < size; i++)
            {
                int j = 0;
                while (j < size && matrix[i, j] == 'W')
                {
                    j++;
                }

                if (j == size)
                {
                    countMinimumNumberOfInversions++;
                    for (j = 0; j < size; j++)
                    {
                        matrix[i, j] = 'B';
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                int j = 0;
                while (j < size && matrix[j, i] == 'W')
                {
                    j++;
                }

                if (j == size)
                {
                    countMinimumNumberOfInversions++;
                    for (j = 0; j < size; j++)
                    {
                        matrix[j, i] = 'B';
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (matrix[i, 0] == 'W')
                {
                    countMinimumNumberOfInversions++;
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = matrix[i, j] == 'W' ? 'B' : 'W';
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (matrix[0, i] == 'W')
                {
                    countMinimumNumberOfInversions++;
                    for (int j = 0; j < size; j++)
                    {
                        matrix[j, i] = matrix[j, i] == 'W' ? 'B' : 'W';
                    }
                }
            }

            return countMinimumNumberOfInversions;
        }
    }   
}
