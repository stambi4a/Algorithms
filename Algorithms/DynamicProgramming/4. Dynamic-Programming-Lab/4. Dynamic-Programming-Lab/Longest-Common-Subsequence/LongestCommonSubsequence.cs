namespace Longest_Common_Subsequence
{
    using System;
    using System.Linq;
    using System.Text;

    public class LongestCommonSubsequence
    {
        public static void Main()
        {
            var firstStr = "strcture";
            var secondStr = "struture";

            var lcs = FindLongestCommonSubsequence(firstStr, secondStr);

            Console.WriteLine("Longest common subsequence:");
            Console.WriteLine("  first  = {0}", firstStr);
            Console.WriteLine("  second = {0}", secondStr);
            Console.WriteLine("  lcs    = {0}", lcs);
        }

        public static string FindLongestCommonSubsequence(string firstStr, string secondStr)
        {
            int firstLen = firstStr.Length + 1;
            int secondLen = secondStr.Length + 1;
            var lcs = new int[firstLen, secondLen];
            for (int i = 1; i < firstLen; i++)
            {
                for (int j = 1; j < secondLen; j++)
                {
                    if (firstStr[i - 1] == secondStr[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;                       
                    }
                    else
                    {
                        lcs[i, j] = Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                    }
                }
            }

            string sequence = RetrieveLcs(firstStr, secondStr, lcs);
            if (sequence == null)
            {
                return string.Empty;
            }

            return sequence;
        }

        private static string RetrieveLcs(string firstStr, string secondStr, int[,] lcs)
        {
            int rows = firstStr.Length;
            int columns = secondStr.Length;
            int maxSubsequenceLength = lcs[rows, columns];
            if (maxSubsequenceLength == 0)
            {
                return null;
            }

            int subsequenceLength = maxSubsequenceLength;
            char[] sequence = new char[maxSubsequenceLength];
            while (subsequenceLength > 0)
            {
                while (rows > 1 && lcs[rows, columns] == subsequenceLength)
                {
                    rows--;
                }

                if (lcs[rows, columns] < subsequenceLength)
                {
                    rows++;
                }

                while (columns > 1 && lcs[rows, columns] == subsequenceLength)
                {
                    columns--;
                }

                if (lcs[rows, columns] < subsequenceLength)
                {
                    columns++;
                }               

                sequence[subsequenceLength - 1] = firstStr[rows - 1];
                subsequenceLength--;
                columns--;
            }

            return new String(sequence);
        }
    }
}
