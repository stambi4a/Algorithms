namespace SubsetsOfStrings
{
    using System;
    using System.Linq;

    internal class StringSubsets
    {
        private static int countPermutattions = 1;
        static void Main(string[] args)
        {
            var set = new[] { "test", "rock", "fun", "hand", "scissors" };
            int n = set.Length;
            int k = int.Parse(Console.ReadLine());
            int[] arr = Enumerable.Range(1, k).ToArray();
            PrintCombination(arr, set);
            int[] free = Enumerable.Range(1, n).ToArray();
            GenerateRepetitions(arr, free, set,  k, n);
            Console.WriteLine("Count of permutations:" + countPermutattions);
        }

        private static void GenerateRepetitions(int[] arr, int[] free, string[] set,  int k, int n)
        {
            while (true)
            {
                int i = k - 1;
                while (i >= 0 && arr[i] == free[n - k + i])
                {
                    i--;
                }

                if (i < 0)
                {
                    break;
                }

                arr[i]++;
                for (int j = i + 1; j < k; j++)
                {
                    arr[j] = free[j];
                }

                i = 0;
                while (i < k - 1 && arr[i] < arr[i + 1])
                {
                    i++;
                }
                if (i == k - 1)
                {
                    PrintCombination(arr, set);
                    countPermutattions++;
                }
            }
        }

        private static void PrintCombination(int[] arr, string[] set)
        {
            Console.WriteLine("(" + string.Join(", ", arr.Select(x=>set[x - 1])) + ")");
        }
    }
}
