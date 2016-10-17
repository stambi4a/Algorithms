namespace Parenthesis
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private const char Opening = '(';
        private const char Closing = ')';

        private static HashSet<string> sequences;
        private static void Main(string[] args)
        {
            sequences = new HashSet<string>();
            int countPairs = int.Parse(Console.ReadLine());         
            char[] sequence = new char[2 * countPairs];
            GenerateParenthesisSequence(0, 0, 0, sequence);
            Console.WriteLine(string.Join("\n", sequences));
        }

        private static void GenerateParenthesisSequence(int index, int indexOpening, int indexClosing, char[] sequence)
        {
            if (index == sequence.Length)
            {
                sequences.Add(new string(sequence));
                return;
            }

            if (indexOpening < sequence.Length / 2)
            {
                sequence[index] = Opening;
                GenerateParenthesisSequence(index + 1, indexOpening + 1, indexClosing, sequence);
            }

            if (indexClosing < indexOpening)
            {
                sequence[index] = Closing;
                GenerateParenthesisSequence(index + 1, indexOpening, indexClosing + 1, sequence);
            }
        }
    }
}
