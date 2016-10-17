namespace Bridges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var bridges = new SortedDictionary<int, int>();
            string[] output = Enumerable.Repeat("X", numbers.Length).ToArray();
            int countBridges = 0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int j = i + 1;
                while (j < numbers.Length && numbers[j] != numbers[i])
                {
                    j++;
                }

                if (j < numbers.Length)
                {
                    bridges.Add(j, i);
                }
            }

            int lastIndex = 0;
            foreach (var bridge in bridges)
            {
                if (bridge.Value >= lastIndex)
                {
                    output[bridge.Value] = numbers[bridge.Value].ToString();
                    output[bridge.Key] = numbers[bridge.Key].ToString();
                    countBridges++;
                    lastIndex = bridge.Key;
                }
            }
            if (countBridges == 0)
            {
                Console.WriteLine("No bridges found");
            }
            else if (countBridges == 1)
            {
                Console.WriteLine("1 bridge found");
            }
            else
            {
                Console.WriteLine("{0} bridges found", countBridges);
            }

            Console.WriteLine(string.Join(" ", output));
        }
    }
}
