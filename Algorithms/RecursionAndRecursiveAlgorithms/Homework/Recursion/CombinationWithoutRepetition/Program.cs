namespace CombinationWithoutRepetition
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("n=");
            int n = int.Parse(Console.ReadLine());
            Console.Write("k=");
            int k = int.Parse(Console.ReadLine());
            int[] array = new int[k];
            PrintNestedLoops(array, 0, k, n, 0);
        }

        private static void PrintArray(int[] array)
        {
            foreach (var i in array)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private static void PrintNestedLoops(int[] array, int index, int k, int n, int level)
        {
            if (index == k)
            {
                PrintArray(array);
                return;
            }

            array[index] = level;
            for (int i = level; i < n; i++)
            {
                array[index]++;
                PrintNestedLoops(array, index + 1, k, n, array[index]);
            }


        }
    }
}
