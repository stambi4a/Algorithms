namespace Guitar
{
    using System;
    using System.Linq;

    internal class Program
    {
        private static int maximalVolume;
        private static int currentMaximalVolume;
        private static int[] intervals;
        private static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            intervals = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                intervals[i] = int.Parse(input[i]);
            }
            int initialVolume = int.Parse(Console.ReadLine());
            maximalVolume = int.Parse(Console.ReadLine());
            currentMaximalVolume = -1;
            FindMaximalPossibleVolume(0, initialVolume);
            Console.WriteLine(currentMaximalVolume);
        }

        private static void FindMaximalPossibleVolume(int index, int currentVolume)
        {
            if (index == intervals.Length)
            {
                if (currentVolume > currentMaximalVolume)
                {
                    currentMaximalVolume = currentVolume;
                }

                return;
            }

            int nextInterval = intervals[index];
            if (nextInterval <= maximalVolume - currentVolume)
            {
                currentVolume += nextInterval;
                FindMaximalPossibleVolume(index + 1, currentVolume);
                currentVolume -= nextInterval;
            }

            if (nextInterval <= currentVolume)
            {
                currentVolume -= nextInterval;
                FindMaximalPossibleVolume(index + 1, currentVolume);
                currentVolume += nextInterval;
            }
        }
    }
}
