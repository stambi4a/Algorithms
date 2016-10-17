namespace SumWithLimitedCoins
{
    using System;
    using System.Linq;

    internal class SumWithLimitedCoins
    {
        private static int countSubsets = 0;
        private static void Main(string[] args)
        {
            int targetSum = int.Parse(Console.ReadLine());
            int[] seq = Console.ReadLine().Split(new[] { ',', ' ' }).Select(int.Parse).ToArray();
            Array.Sort(seq);
            GenerateVariationsWithRepetitions(-1, seq, targetSum, targetSum);
            Console.WriteLine(countSubsets);
        }

        static void GenerateVariationsWithRepetitions(int index, int[] seq, int targetSum, int difference)
        {
            if (difference == 0)
            {
                countSubsets++;
                return;
            }

            if (difference < 0)
            {
                return;
            }

            for (int i = index + 1; i < seq.Length; i++)
            {
                if (i > index + 1 && seq[i] == seq[i - 1])
                {
                    continue;
                }

                difference -= seq[i];
                GenerateVariationsWithRepetitions(i, seq, targetSum, difference);
                difference += seq[i];

            }

        }
    }
}
