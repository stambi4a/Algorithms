namespace TowerOfHanoy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static Stack<int> source;
        private static Stack<int> destination;
        private static Stack<int> swap;
        private static int stepsTaken = 0;

        private static void Main(string[] args)
        {
            destination = new Stack<int>();
            swap = new Stack<int>();
            int n = int.Parse(Console.ReadLine());
            source = new Stack<int>(Enumerable.Range(1, n).Reverse());
            PrintRods();
            MoveDiscs(n, source, destination, swap);
        }

        private static void MoveDiscs(int bottomDisc, Stack<int> source, Stack<int> destination, Stack<int> swap)
        {
            if (bottomDisc == 1)
            {
                stepsTaken++;
                destination.Push(source.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disc {bottomDisc}");
                PrintRods();
            }
            else
            {

                MoveDiscs(bottomDisc - 1, source, swap, destination);
                destination.Push(source.Pop());
                stepsTaken++;
                Console.WriteLine($"Step #{stepsTaken}: Moved disc {bottomDisc}");
                PrintRods();
                MoveDiscs(bottomDisc - 1, swap, destination, source);
            }
        }

        private static void PrintRods()
        {
            Console.WriteLine("Source: {0}", source.Count > 0 ? string.Join(", ", source.Reverse()) : String.Empty);
            Console.WriteLine("Destination: {0}", destination.Count > 0 ? string.Join(", ", destination.Reverse()) : String.Empty);
            Console.WriteLine("Swap: {0}", swap.Count > 0 ? string.Join(", ", swap.Reverse()) : String.Empty);
            Console.WriteLine();
        }
    }
}
