namespace RecursiveArraySum
{
    using System;
    using System.Xml.Schema;

    internal class ArraySum
    {
        private static void Main(string[] args)
        {
            int[] array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = i + 1;
            }

            int sum = FindSum(array, 0);
            Console.WriteLine(sum);
        }

        private static int FindSum(int[] array, int index)
        {
            if (index == array.Length - 1)
            {
                return array[array.Length - 1];
            }

            return array[index] + FindSum(array, index + 1);
        }
    }
}
