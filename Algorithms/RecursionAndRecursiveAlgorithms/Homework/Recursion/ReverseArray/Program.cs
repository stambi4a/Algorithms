namespace ReverseArray
{
    using System;
    using System.Linq;
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            ReverseArray(array, 0);
            array.ToList().ForEach(x => Console.Write(x + " "));
        }


        private static void ReverseArray(int[] array, int index)
        {
            if (index >= array.Length / 2)
            {
                return;
            }

            int swap = array[index];
            array[index] = array[array.Length - 1 - index];
            array[array.Length - 1 - index] = swap;
            ReverseArray(array, index + 1);
        }
    }
}
