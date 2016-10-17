namespace CombinationsWithRepetitions
{
    using System;

    internal class CombinationsWithReps
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] arr = new int[k];
            GenerateRepetitions(arr, n, 0);
        }

        private static void GenerateRepetitions(int[] arr, int sizeOfSet, int index)
        {
            if (index == arr.Length)
            {
                PrintCombination(arr);
                return;
            }

            for (int i = index + 1; i <= sizeOfSet; i++)
            {
                arr[index] = i;
                GenerateRepetitions(arr, sizeOfSet, index + 1);
            }
        }

        private static void PrintCombination(int[] arr)
        {
            Console.WriteLine("(" + string.Join(", ", arr) + ")");
        }
    }
}
