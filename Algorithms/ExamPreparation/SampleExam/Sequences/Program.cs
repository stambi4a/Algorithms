namespace Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Schema;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var sequence = new List<string>();
            string input = Console.ReadLine();
            int length = input.Length;
            var charOccurrence = new Dictionary<char, int>();
            for (int i = 0; i < length; i++)
            {
                if (!charOccurrence.ContainsKey(input[i]))
                {
                    charOccurrence.Add(input[i], 0);
                }
                charOccurrence[input[i]]++;
            }

            foreach(var ch in charOccurrence.Keys)
            {
                sequence.Add(new string(ch, charOccurrence[ch]));
            }

            string[] permutation = new string[sequence.Count];
            CreateSequencePermutation(0, new bool[sequence.Count], sequence, permutation);
        }

        private static void CreateSequencePermutation(int index, bool[] visited, List<string> sequence, string[] permutation)
        {
            if (index == sequence.Count)
            {
                Console.WriteLine(string.Join("", permutation));
            }

            for (int i = 0; i < sequence.Count; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    permutation[index] = sequence[i];
                    CreateSequencePermutation(index + 1, visited, sequence, permutation);
                    visited[i] = false;
                }
            }
        }
    }
}
