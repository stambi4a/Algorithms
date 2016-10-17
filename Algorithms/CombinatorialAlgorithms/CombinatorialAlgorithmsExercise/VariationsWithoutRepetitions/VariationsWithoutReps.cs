namespace VariationsWithoutRepetitions
{
    using System;
    using System.Linq;

    class VariationsWithoutReps
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] arr = new int[k];
            int[] free = Enumerable.Range(1, n).ToArray();
            GenerateRepetitions(arr, n, 0, free);
        }

        private static void GenerateRepetitions(int[] arr, int sizeOfSet, int index, int[] free)
        {
            if (index == arr.Length)
            {
                PrintVariation(arr);
                return;
            }

            for (int i = index; i < sizeOfSet; i++)
            {
                arr[index] = free[i];
                Swap(ref free[i],ref free[index]);
                GenerateRepetitions(arr, sizeOfSet, index + 1, free);
                Swap(ref free[i], ref free[index]);
            }
        }

        private static void Swap(ref int v1,ref int v2)
        {
            int swap = v1;
            v1 = v2;
            v2 = swap;
        }

        private static void PrintVariation(int[] arr)
        {
            Console.WriteLine("(" + string.Join(", ", arr) + ")");
        }
    }
}
