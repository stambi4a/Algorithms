namespace IterativePermutationsWithhoutRepetitions
{
    using System;
    using System.Linq;

    class Permutations
    {
        private static int countPermutattions = 1;
        private static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());               
            GeneratePermutations(n);
            Console.WriteLine("Count of permutations:" + countPermutattions);
        }

        private static void GeneratePermutations(int n)
        {
            int[] arr = Enumerable.Range(1, n).ToArray();
            PrintPermutation(arr);
            int fact = CalculateFactorial(n);
            for (int k = 1; k < fact; k++)
            {
                int i = n - 1;
                while (arr[i - 1] >= arr[i])
                {
                    i = i - 1;
                }       

                int j = n;
                while (arr[j - 1] <= arr[i - 1])
                {
                    j = j - 1;
                }

                Swap(ref arr[i - 1], ref arr[j - 1]);    // swap arrs at positions (i-1) and (j-1)

                i++; j = n;
                while (i < j)
                {
                    Swap(ref arr[i - 1], ref arr[j - 1]);
                    i++;
                    j--;
                }

                countPermutattions++;
                PrintPermutation(arr);
            }
            
        }

        private static void Swap(ref int v1, ref int v2)
        {
            int old = v1;
            v1 = v2;
            v2 = old;
        }

        private static int CalculateFactorial(int i)
        {
            if (i == 0)
            {
                return 1;
            }

            return i * CalculateFactorial(i - 1);
        }     

        private static void PrintPermutation(int[] arr)
        {
            Console.WriteLine($"({String.Join(", ", arr)})");
        }
    }
}
