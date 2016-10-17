namespace NestedLoops
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] array = new int[n];
            PrintNestedLoops(array, 0, n);
        }

        private static void PrintArray(int[] array)
        {
            foreach (var i in array)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private static void PrintNestedLoops(int[] array, int index, int n)
        {
            if (index == n)
            {
                PrintArray(array);
                return;
            }


            for (int i = 0; i < n; i++)
            {
                array[index]++;
                PrintNestedLoops(array, index + 1, n);               
            }

            array[index] = 0;
        }
    }
}
