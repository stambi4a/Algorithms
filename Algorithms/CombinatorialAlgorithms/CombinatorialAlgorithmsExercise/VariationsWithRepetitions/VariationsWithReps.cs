namespace VariationsWithRepetitions
{
    using System;

    class VariationsWithReps
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
                PrintVariation(arr);
                return;
            }

            for (int i = 1; i <= sizeOfSet; i++)
            {
                arr[index] = i;
                GenerateRepetitions(arr, sizeOfSet, index + 1);
            }
        }

        private static void PrintVariation(int[] arr)
        {
            Console.WriteLine("(" + string.Join(", ", arr) + ")");
        }
    }
}
