using System;
using System.Collections.Generic;

namespace LongestZigzagSequence
{
    using System.Linq;

    class LongestZigzagSequence
    {
        const int NoPrevious = -1;
        static void Main()
        {
            /*int[] seq = { 24, 5, 31, 3, 3, 342, 51, 114, 52, 55, 56, 58 };*/
            int[] seq = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[,] len = new int[2, seq.Length];
            int[,] prev = new int[2, seq.Length];

            int[] bestIndex = CalculateLongestZigzagSubsequence(seq, len, prev);

            int index = 0;
            if (bestIndex[0] <= bestIndex[1])
            {
                index = 1;
            }

            int[] length = new int[seq.Length];
            int[] previous = new int[seq.Length];
            for (int i = 0; i < seq.Length; i++)
            {
                length[i] = len[index, i];
                previous[i] = prev[index, i];
            }

            Console.WriteLine("seq[]  = " + String.Join(", ", seq));
            Console.WriteLine("len[]  = " + String.Join(", ", length));
            Console.WriteLine("prev[] = " + String.Join(", ", previous));

            PrintLongestZigzagSubsequence(seq, previous, bestIndex[index]);
        }

        private static int[] CalculateLongestZigzagSubsequence(
            int[] seq, int[,] len, int[,] prev)
        {

            int[] bestIndex = new int[2];
            int[] bestLength = new int[2];
            for (int x = 0; x < seq.Length; x++)
            {
                len[0, x] = 1;
                len[1, x] = 1;
                prev[0, x] = NoPrevious;
                prev[1, x] = NoPrevious;
                for (int i = 0; i <= x - 1; i++)
                {
                    if (((len[0, i] - 1) % 2 == 0 && seq[i] < seq[x] || (len[0, i] - 1) % 2 == 1 && seq[i] > seq[x])  && 1 + len[0, i] > len[0, x])
                    {
                        len[0, x] = 1 + len[0, i];
                        prev[0, x] = i;
                        if (len[0, x] > bestLength[0])
                        {
                            bestLength[0] = len[0, x];
                            bestIndex[0] = x;
                        }
                    }

                    if (((len[1, i] - 1) % 2 == 0 && seq[i] > seq[x] || (len[1, i] - 1) % 2 == 1 && seq[i] < seq[x]) && 1 + len[1, i] > len[1, x])
                    {
                        len[1, x] = 1 + len[1, i];
                        prev[1, x] = i;
                        if (len[1, x] > bestLength[0])
                        {
                            bestLength[1] = len[1, x];
                            bestIndex[1] = x;
                        }
                    }
                }
            }

            return bestIndex;
        }

        static void PrintLongestZigzagSubsequence(int[] seq, int[] prev, int index)
        {
            List<int> lis = new List<int>();
            while (index != NoPrevious)
            {
                lis.Add(seq[index]);
                index = prev[index];
            }
            lis.Reverse();
            Console.WriteLine("subsequence = [{0}]", string.Join(", ", lis));
        }
    }
}
