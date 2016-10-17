namespace Bridges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    { 
        private static void Main(string[] args)
        {
            int[] north = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] south = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var correspondingIndices = FindCorrespondingIndices(north, south);
            var bridgesPerNorthIndex = FindAllBridgesPerIndex(correspondingIndices);
            int maxCountBridges = FindMaxCountOfBridges(bridgesPerNorthIndex);
            Console.WriteLine(maxCountBridges);
        }

        private static int FindMaxCountOfBridges(Dictionary<int, SortedDictionary<int, int>> bridgesPerNorthIndex)
        {
            var maxValue = bridgesPerNorthIndex.SelectMany(x => x.Value).Max(x => x.Value);
            return maxValue;
        }

        private static Dictionary<int, SortedDictionary<int, int>> FindAllBridgesPerIndex(Dictionary<int, SortedSet<int>> correspondingIndices)
        {
            var bridgesPerNorthIndex = new Dictionary<int, SortedDictionary<int, int>>();

            foreach (var index in correspondingIndices.Keys)
            {
                bridgesPerNorthIndex.Add(index, new SortedDictionary<int, int>());
                if (correspondingIndices[index].Count == 0)
                {
                    continue;
                }

                foreach (var correspondingIndex in correspondingIndices[index])
                {
                    bridgesPerNorthIndex[index].Add(correspondingIndex, 0);
                }

                foreach (var correspondingIndex in correspondingIndices[index])
                {
                    int maxCountBridges = 0;
                    for (int i = index - 1; i >= 0; i--)
                    {
                        int countBridges = 0;
                        foreach (var corresponding in correspondingIndices[i])
                        {
                            if (corresponding > correspondingIndex)
                            {
                                break;
                            }

                            countBridges = bridgesPerNorthIndex[i][corresponding];
                        }

                        if (countBridges > maxCountBridges)
                        {
                            maxCountBridges = countBridges;
                        }
                    }

                    maxCountBridges++;
                    bridgesPerNorthIndex[index][correspondingIndex] = bridgesPerNorthIndex[index][correspondingIndex]
                                                                      > maxCountBridges
                                                                          ? bridgesPerNorthIndex[index][
                                                                              correspondingIndex]
                                                                          : maxCountBridges;
                    foreach (var otherIndex in correspondingIndices[index])
                    {
                        if (otherIndex > correspondingIndex)
                        {
                            bridgesPerNorthIndex[index][otherIndex] = bridgesPerNorthIndex[index][otherIndex]
                                                                      > ++maxCountBridges
                                                                          ? bridgesPerNorthIndex[index][
                                                                              otherIndex]
                                                                          : maxCountBridges;
                        }
                    }
                }
            }

            return bridgesPerNorthIndex;
        }

        private static Dictionary<int, SortedSet<int>> FindCorrespondingIndices(int[] north, int[] south)
        {
            int length = north.Length;
            var correspondingIndices = new Dictionary<int, SortedSet<int>>();
            for (int i = 0; i < length; i++)
            {
                correspondingIndices.Add(i, new SortedSet<int>());
                for (int j = 0; j < south.Length; j++)
                {
                    if (north[i] == south[j])
                    {
                        correspondingIndices[i].Add(j);
                    }
                }
            }

            return correspondingIndices;
        }
    }
}
