namespace Problem01
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Program
    {
        static List<string> sequences;
        private static List<int> indices;
        private static int[] lineIndices;
        private static string numString;
        static void Main(string[] args)
        {
            sequences = new List<string>();
            string[] numbers = Console.ReadLine().Split();
            numString = string.Join("", numbers);
            indices = new List<int>();
            List<string> parts = new List<string>();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == "1")
                {
                    indices.Add(i);
                }
            }
            lineIndices = new int[indices.Count - 1];
            GenerateSequences(1);
            Console.WriteLine(string.Join("\n", sequences));

        }

        static void GenerateSequences(int index)
        {
            if (index == indices.Count)
            {
                int count = indices.Count;
                StringBuilder sequence = new StringBuilder();
                sequence.Append(numString.Substring(0, indices[0] + 1));
                int j = 0;
                int i = 0;
                int current = indices[0] + 1;
                while (i < count && j < count - 1)
                {
                    while (current < lineIndices[i])
                    {
                        sequence.Append("0");
                        current++;
                    }

                    i++;
                    sequence.Append("|");

                    while (current < indices[i])
                    {
                        sequence.Append("0");
                        current++;
                    }

                    sequence.Append("1");
                    current++;
                    j++;
                }

                for (i = current; i < count; i++)
                {
                    sequence.Append("0");
                }

                sequences.Add(sequence.ToString());
                return;
            }

            for (int i = indices[index - 1] + 1; i <= indices[index]; i++)
            {
                lineIndices[index - 1] = i;
                GenerateSequences(index + 1);
            }
        }
    }
}
