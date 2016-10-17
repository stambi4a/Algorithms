namespace PermutationsWithoutRepetitions
{
    using System;
    using System.Linq;

    internal class Permutations
    {
        private static int countPermutattions = 0;
        private static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            int[] free = Enumerable.Range(1, n).ToArray();
            GeneratePermutation(0, arr, free);
            Console.WriteLine("Count of permutations:" + countPermutattions);
        }

        private static void GeneratePermutation(int index, int[] arr, int[] free)
        {
            if (index == arr.Length)
            {
                PrintPermutation(arr);
                countPermutattions++;
            }

            for (int i = index; i < arr.Length; i++)
            {
                arr[index] = free[i];
                Swap(ref free[i], ref free[index]);
                GeneratePermutation(index + 1, arr, free);
                Swap(ref free[i], ref free[index]);
            }
        }

        private static void Swap(ref int v1, ref int v2)
        {
            int swap = v1;
            v1 = v2;
            v2 = swap;
        }

        private static void PrintPermutation(int[] arr)
        {
            Console.WriteLine($"({String.Join(", ", arr)})");
        }
    }
}
