namespace Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Program
    {
        private static int sum;
        private static StringBuilder sums;
        private static List<int> numbers;
       /* private static bool[] visited;
        private static int[] variation;*/

        private static void Main(string[] args)
        {
            sum = int.Parse(Console.ReadLine());
            sums = new StringBuilder();
            numbers = new List<int>();
            FindAllNumberSumsRecursion(sum);
            /*FindAllNumberSumsRecursionSecondWay(1, 0);
            PrintSums();*/
            Console.WriteLine(sums);
        }


        private static void FindAllNumberSumsRecursion(int currentSum)
        {
            for (int i = 1; i <= currentSum; i++)
            {
                numbers.Add(i);
                currentSum -= i;
                for (int j = 0; j < numbers.Count - 1; j++)
                {
                    sums.Append(numbers[j] + " ");
                }

                sums.Append(numbers[numbers.Count - 1]);
                sums.AppendLine();
                FindAllNumberSumsRecursion(currentSum);
                numbers.RemoveAt(numbers.Count - 1);
                currentSum += i;
            }

            /*numbers.Add(upperLimit);
            Console.WriteLine(string.Join(" ", numbers));
            numbers.RemoveAt(numbers.Count - 1);*/
        }

       /* private static void PrintSums()
        {
            foreach (var sum in sums)
            {
                Console.WriteLine(sum);
            }
        }*/

       /* private static void FindAllNumberSumsRecursionSecondWay(int index, int currentSum)
        {
            if (currentSum == sum)
            {
                return;
            }

            int upperLimit = sum - currentSum;
            for (int i = index; i <= upperLimit; i++)
            {
                numbers.Add(i);
                currentSum += i;
                variation = new int[numbers.Count];
                visited = new bool[numbers.Count];
                variation = new int[numbers.Count];
                FindAllVariations(0);
                FindAllNumberSumsRecursionSecondWay(i, currentSum);
                numbers.RemoveAt(numbers.Count - 1);
                currentSum -= i;
            }
        }
*/
        /*private static void FindAllVariations(int index)
        {
            if (index == numbers.Count)
            {
                sums.Add(string.Join(" ", variation));
                return;
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                if (!visited[i])
                {
                    while (i < numbers.Count - 1 && numbers[i] == numbers[i + 1] && !visited[i + 1])
                    {
                        i++;
                    }

                    visited[i] = true;
                    variation[index] = numbers[i];
                    FindAllVariations(index + 1);
                    visited[i] = false;
                }
            }
        }*/
    }
}
