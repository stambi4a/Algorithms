namespace ConnectingCables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class ConnectingCables
    {
        private const string MaximumPairs = "Maximum pairs connected: {0}";
        private const string ConnectedPairs = "Connected pairs: {0}";
        private static void Main(string[] args)
        {
            /*string input = Regex.Match(Console.ReadLine(), "{(\\d,)+\\d}").ToString();
            Console.WriteLine(input);*/
            int[] cablesFirstSide = Regex.Match(Console.ReadLine(), "{(\\d,)+\\d}").
                ToString().
                Split(new[] {',', '{', '}'}, StringSplitOptions.RemoveEmptyEntries).
                Select(int.Parse).
                ToArray();
            int[] cablesSecondSide = Regex.Match(Console.ReadLine(), "{(\\d,)+\\d}").
                ToString().
                Split(new[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries).
                Select(int.Parse).
                ToArray();

            int length = cablesSecondSide.Length;
            Dictionary<int, int> correspondingIndices = new Dictionary<int, int>();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (cablesSecondSide[j] == cablesFirstSide[i])
                    {
                        correspondingIndices.Add(i, j);
                        break;
                    }
                }
            }

            Dictionary<int, List<int>> maxPairsForIndex = new Dictionary<int, List<int>>();
            maxPairsForIndex.Add(length - 1, new List<int> { cablesFirstSide[length - 1]});
            int maxPairs = 1;
            int indexMaxPairs = length - 1;
            for (int i = length - 2; i >= 0; i --)
            {
                int maxPairsThisIndex = 1;
                int index = length - 1;
                maxPairsForIndex.Add(i, new List<int> { cablesFirstSide [i]});
                for (int j = i + 1; j < length - 1; j++)
                {
                    if (correspondingIndices[i] < correspondingIndices[j])
                    {
                        int pairs = maxPairsForIndex[j].Count + 1;
                        if (pairs > maxPairsThisIndex)
                        {
                            maxPairsThisIndex = pairs;
                            index = j;
                        }
                    }
                }

                maxPairsForIndex[i].AddRange(maxPairsForIndex[index]);
                if (maxPairs < maxPairsThisIndex)
                {
                    maxPairs = maxPairsThisIndex;
                    indexMaxPairs = i;
                }
            }

            Console.WriteLine(MaximumPairs, maxPairs);
            Console.WriteLine(ConnectedPairs, string.Join(" ", maxPairsForIndex[indexMaxPairs]));
        }
    }
}
