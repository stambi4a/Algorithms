namespace DividingPresents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DividingPresents
    {
        private const string Difference = "Difference: {0}";
        private const string AlanBob = "Alan: {0} Bob: {1}";
        private const string AlanTakes = "Alan takes: {0}";
        private const string BobTakes = "Bob takes the rest.";

        private static void Main(string[] args)
        {
            /*int[] sequence = { 3, 2, 3, 2, 2, 77, 89, 23, 90, 11 };*/
            int[] sequence = Console.ReadLine().Split(new[] {',', ' '}).Select(int.Parse).ToArray();
            Dictionary<int, int> possibleSums = FindAllPossibleSums(sequence);
            int sumSequence = sequence.Sum();
            int closestSumToHalf = FindClosestSumToHalf(sumSequence, possibleSums);
            var subset = FindAlanPresents(possibleSums, closestSumToHalf);
            PrintPresentsDivision(closestSumToHalf, sumSequence, subset);
        }

        private static void PrintPresentsDivision(int closestSumToHalf, int sumSequence, List<int> subset)
        {
            Console.WriteLine(Difference, sumSequence - 2 * closestSumToHalf);
            Console.WriteLine(AlanBob, closestSumToHalf, sumSequence - closestSumToHalf);
            Console.WriteLine(AlanTakes, string.Join(" ", subset));
            Console.WriteLine(BobTakes);
        }

        private static List<int> FindAlanPresents(Dictionary<int, int> possibleSums, int closestSumToHalf )
        {
            var subset = new List<int>();
            while (closestSumToHalf > 0)
            {
                int presentValue = possibleSums[closestSumToHalf];
                subset.Add(presentValue);
                closestSumToHalf -= presentValue;
            }

            return subset;
        }

        private static int FindClosestSumToHalf(int sumSequence, Dictionary<int, int> possibleSums)
        {
            int closestSumToHalf = sumSequence / 2;
            while (true)
            {
                if (possibleSums.ContainsKey(closestSumToHalf))
                {
                    return closestSumToHalf;
                }

                closestSumToHalf--;
            }
        }

        private static Dictionary<int, int> FindAllPossibleSums(int[] sequence)
        {
            Dictionary<int, int> possibleSums = new Dictionary<int, int> { { 0, 0 } };
            for (int i = 0; i < sequence.Length; i++)
            {
                var newSums = new Dictionary<int, int>();
                foreach (var sum in possibleSums.Keys)
                {
                    int newSum = sum + sequence[i];
                    if (!possibleSums.ContainsKey(newSum))
                    {
                        newSums.Add(newSum, sequence[i]);
                    }                    
                }

                foreach (var sum in newSums)
                {
                    possibleSums.Add(sum.Key, sum.Value);
                }
            }

            return possibleSums;
        }

    }
}
