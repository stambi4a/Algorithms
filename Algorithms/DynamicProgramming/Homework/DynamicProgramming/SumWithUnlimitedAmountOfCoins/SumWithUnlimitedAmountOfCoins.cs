namespace SumWithUnlimitedAmountOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SumWithUnlimitedAmountOfCoins
    {
        private static int countSubsets = 0;
        private static void Main(string[] args)
        {
            int targetSum = int.Parse(Console.ReadLine());
            int[] seq = Console.ReadLine().Split(new []{ ',', ' ' }).Select(int.Parse).ToArray();
            Array.Sort(seq);
            GenerateVariationsWithRepetitions(0, seq, targetSum, targetSum);
            Console.WriteLine(countSubsets);
        }

        static void GenerateVariationsWithRepetitions(int index, int[] seq, int targetSum,int difference)
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

            for (int i = index; i < seq.Length; i++)
            {
                difference -= seq[i];
                GenerateVariationsWithRepetitions(i, seq, targetSum, difference);
                difference += seq[i];

            }
                                
        }
    }
}
