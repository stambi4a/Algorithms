namespace PermutationsWithRepetitions
{
    using System;
    using System.Linq;

    internal class PermutationsWithReps
    {
        private static int countPermutattions = 0;
        private static void Main(string[] args)
        {
            int[] ps = { 1, 3, 5, 5 };
            GeneratePermutation(ps, 0, ps.Length);
            Console.WriteLine("Count of permutations:" + countPermutattions);
        }

        public static void GeneratePermutation(int[] ps, int start, int n)
        {
            PrintPermutation(ps);
            countPermutattions++;
            int tmp = 0;

            if (start < n)
            {
                for (int i = n - 2; i >= start; i--)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (ps[i] != ps[j])
                        {
                            // swap ps[i] <--> ps[j]
                            tmp = ps[i];
                            ps[i] = ps[j];
                            ps[j] = tmp;

                            GeneratePermutation(ps, i + 1, n);
                        }
                    }

                    // Undo all modifications done by
                    // recursive calls and swapping
                    tmp = ps[i];
                    for (int k = i; k < n - 1;)
                        ps[k] = ps[++k];
                    ps[n - 1] = tmp;
                }
            }
        }

        private static void Swap(ref int v1, ref int v2)
        {
            int swap = v1;
            v1 = v2;
            v2 = swap;
        }

        private static void PrintPermutation(int[] arr)
        {
            Console.WriteLine($"({String.Join(", ", arr)})");
        }
    }
}