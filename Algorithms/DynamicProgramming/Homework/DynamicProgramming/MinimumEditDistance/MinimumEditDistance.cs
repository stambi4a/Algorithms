namespace MinimumEditDistance
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal class MinimumEditDistance
    {
        private const string MinimumDistance = "Minimum edit distance: {0}";
        private const string Insert = "INSERT({0}, {1})";
        private const string Delete = "DELETE({0})";
        private const string Replace = "REPLACE({0}, {1})";
        private static void Main(string[] args)
        {
            FindMinimumEditDistance();
        }

        private static void EditString(
            string first,
            string second,
            int[] firstIndices,
            int[] secondIndices, 
            int costReplace, 
            int costInsert, 
            int costDelete)
        {
            List<string> operations = new List<string>();
            int length = firstIndices.Length;
            int editDistance = 0;
            int indexFirst = 0;
            int currentIndex = 0;
            int indexSecond = 0;
            bool replaceIsBetterThanInsertAndDelete = costReplace <= costDelete + costInsert;
            while (currentIndex < length)
            {              
                int difference = firstIndices[currentIndex] - secondIndices[currentIndex];
                if (currentIndex > 0)
                {
                    difference = difference - firstIndices[currentIndex - 1] + secondIndices[currentIndex - 1];
                }

                for (int k = difference; k < 0; k++)
                {
                    operations.Add(string.Format(Insert, indexFirst, second[indexSecond]));
                    editDistance += costInsert;
                    indexSecond++;
                }

                for (int k = 0; k < difference; k++)
                {
                    operations.Add(string.Format(Delete, indexFirst));
                    editDistance += costDelete;
                    indexFirst++;
                }

                for (int i = indexSecond; i < secondIndices[currentIndex];  i++)
                {
                    if (replaceIsBetterThanInsertAndDelete)
                    {
                        operations.Add(string.Format(Replace, indexFirst, second[i]));
                        editDistance += costReplace;
                    }
                    else
                    {
                        operations.Add(string.Format(Delete, indexFirst));
                        operations.Add(string.Format(Insert, indexFirst, second[indexSecond]));
                        editDistance += costDelete;
                        editDistance += costInsert;

                    }

                    indexFirst++;
                    indexSecond++;
                }

                indexFirst = firstIndices[currentIndex] + 1;
                indexSecond = secondIndices[currentIndex] + 1;
                currentIndex++;              
            }

            for (int i = firstIndices[length - 1] + 1; i < first.Length; i++)
            {
                operations.Add(string.Format(Delete, i));
                editDistance += costDelete;
            }

            printOperationsAndEditDistance(operations, editDistance);
        }

        private static void printOperationsAndEditDistance(List<string> operations, int editDistance)
        {
            Console.WriteLine(string.Format(MinimumDistance, editDistance));
            if (operations.Count > 0)
            {
                Console.WriteLine(string.Join("\n", operations));
            }            
        }

        private static int[] CreateSequence(string first, string second)
        {
            List<int> sequence = new List<int>();
            int firstLength = first.Length;
            for (int i = 0; i < firstLength; i++)
            {
                char symbol = first[i];
                int index = 0;
                while (true)
                {
                    index = second.IndexOf(symbol, index);
                    if (index == -1)
                    {
                        break;
                    }

                    sequence.Add(index);
                    index++;
                }
            }

            int[] seq = sequence.ToArray();

            return seq;
        }

        private static void FindMinimumEditDistance()
        {
            int costReplace = int.Parse(Regex.Match(Console.ReadLine(), "\\d+").ToString());
            int costInsert = int.Parse(Regex.Match(Console.ReadLine(), "\\d+").ToString());
            int costDelete = int.Parse(Regex.Match(Console.ReadLine(), "\\d+").ToString());
            string first = Regex.Match(Console.ReadLine(), "(?<=\\s)\\w+").ToString();
            string second = Regex.Match(Console.ReadLine(), "(?<=\\s)\\w+").ToString();
            int[] sequence = CreateSequence(first, second);
            int[] longestIncreasingSequence = FindLongestIncreasingSubsequence(sequence);
            int[] firstStringCommonCharsIndices = FindCommonCharacters(first, second, longestIncreasingSequence);
            EditString(first, second, firstStringCommonCharsIndices, longestIncreasingSequence, costReplace, costInsert, costDelete);
        }

        private static int[] FindCommonCharacters(string first, string second, int[] commonCharsIndices)
        {
            int length = commonCharsIndices.Length;
            int index = -1;
            int[] firstStringCommonCharsIndices = new int[length];
            for (int i = 0; i < length; i++)
            {
                char symbol = second[commonCharsIndices[i]];
                index++;
                index = first.IndexOf(symbol, index);
                firstStringCommonCharsIndices[i] = index;
            }

            return firstStringCommonCharsIndices;
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