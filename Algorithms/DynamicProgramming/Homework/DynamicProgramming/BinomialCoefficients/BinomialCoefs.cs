namespace BinomialCoefficients
{
    using System;

    class BinomialCoefs
    {
        private const int Max = 100;
        private static int[,] coefficients = new int[Max, Max];
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int binomialCoefficient = CalculateCoefficient(n, k);
            Console.WriteLine(binomialCoefficient);
        }

        private static int CalculateCoefficient(int n, int k)
        {
            if (k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            if (coefficients[n, k] == 0)
            {
                return CalculateCoefficient(n - 1, k - 1) + CalculateCoefficient(n - 1, k);
            }

            return coefficients[n, k];
        }
    }
}
