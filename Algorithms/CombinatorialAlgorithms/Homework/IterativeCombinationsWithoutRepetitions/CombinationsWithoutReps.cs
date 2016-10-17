namespace IterativeCombinationsWithoutRepetitions
{
    using System;
    using System.Linq;

    class CombinationsWithoutReps
    {
        private static int countPermutattions = 1;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] arr = Enumerable.Range(1, k).ToArray();
            PrintCombination(arr);
            int[] free = Enumerable.Range(1, n).ToArray();
            GenerateRepetitions(arr, free, k, n);
            Console.WriteLine("Count of permutations:" + countPermutattions);
        }

        private static void GenerateRepetitions(int[] arr, int[] free, int k, int n)
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
                    PrintCombination(arr);
                    countPermutattions++;
                }
            }
        }

        private static void PrintCombination(int[] arr)
        {
            Console.WriteLine("(" + string.Join(", ", arr) + ")");
        }
    }
}
