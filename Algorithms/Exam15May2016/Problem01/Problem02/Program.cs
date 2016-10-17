namespace Problem02
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static Dictionary<int, int> maxPrices;
        private static List<int> lengths;
        static int maxPrice = 0;
        private static int connectorPrice;
        static void Main(string[] args)
        {
            maxPrices = new Dictionary<int, int>();
            string[] input = Console.ReadLine().Split();
            lengths = new List<int>();    

            for (int i = 0; i < input.Length; i++)
            {
                int price = int.Parse(input[i]);
                maxPrices.Add(i + 1, price);
            }

            connectorPrice = int.Parse(Console.ReadLine());

            for (int i = 1; i < input.Length; i++)
            {
                CalculatePrices(i + 1, i + 1, 1);
            }

            for (int i = 0; i < maxPrices.Count - 1; i++)
            {
                Console.Write(maxPrices[i + 1] + " ");
            }

            Console.WriteLine(maxPrices[maxPrices.Count]);
        }

        static int CalculatePrice()
        {
            int price = 0;
            for (int i = 0; i < lengths.Count; i++)
            {
                price += maxPrices[lengths[i]];
            }

            price -= connectorPrice * (lengths.Count - 1) * 2;

            return price;
        }

        static void CalculatePrices(int leftLength, int length, int currentLength)
        {
            int otherLength = leftLength / 2;
            for (int i = currentLength; i <= otherLength; i++)
            {
                lengths.Add(i);
                lengths.Add(leftLength - i);
                int price = CalculatePrice();
                if (maxPrices[length] < price)
                {
                    maxPrices[length] = price;
                }

                lengths.RemoveAt(lengths.Count - 1);
                CalculatePrices(otherLength, length, currentLength);
                lengths.RemoveAt(lengths.Count - 1);
            }
        }
    }
}
