using System.Collections.Generic;
using System.Linq;

namespace Longest_Increasing_Subsequence
{
    using System;

    public class LongestIncreasingSubsequence
    {
        public static void Main()
        {
            //var sequence = new[] { 3, 14, 5, 12, 15, 7, 8, 9, 11, 10, 1 };
            var sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var longestSeq = FindLongestIncreasingSubsequence(sequence);
            Console.WriteLine("Longest increasing subsequence (LIS)");
            Console.WriteLine("  Length: {0}", longestSeq.Length);
            Console.WriteLine("  Sequence: [{0}]", string.Join(", ", longestSeq));
        }

        public static int[] FindLongestIncreasingSubsequence(int[] sequence)
        {
            int[] len = new int[sequence.Length];
            int[] prev = new int[sequence.Length];
            int maxlen = 1;
            int lastIndex = -1;
            for (int x = 0; x < sequence.Length; x++)
            {
                len[x] = 1;
                prev[x] = -1;
                for (int i = 0; i < x; i++)
                {
                    if (len[i] >= len[x] && sequence[x] > sequence[i])
                    {
                        len[x]++;
                        prev[x] = i;
                    }

                    if (maxlen < len[x])
                    {
                        maxlen = len[x];
                        lastIndex = x;
                    }
                }             
            }

            if (lastIndex == -1 && sequence.Length > 0)
            {
                lastIndex = 0;
            }

            var longestSequence = new List<int>();
            while (lastIndex != -1)
            {
                longestSequence.Add(sequence[lastIndex]);
                lastIndex = prev[lastIndex];
            }

            longestSequence.Reverse();

            return longestSequence.ToArray();
        }
    }
}
