namespace Problem_1.Fractional_Knapsack_Problem
{
    using System;
    using System.Collections.Generic;

    internal class FractionalKnapsackProblem
    {
        private static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            decimal capacity = decimal.Parse(input[1]);
            input = Console.ReadLine().Split();
            int itemsCount = int.Parse(input[1]);
            var itemsRatioPriceWeight = new SortedDictionary<decimal, Dictionary<decimal, decimal>>(new DecimalComparer());
            for (int i = 0; i < itemsCount; i++)
            {
                input = Console.ReadLine().Split();
                decimal weight = decimal.Parse(input[2]);
                decimal price = decimal.Parse(input[0]);
                decimal ratio = decimal.Round(price / weight, 2);
                if (!itemsRatioPriceWeight.ContainsKey(ratio))
                {
                    itemsRatioPriceWeight.Add(ratio, new Dictionary<decimal, decimal>());
                }

                itemsRatioPriceWeight[ratio].Add(weight, price);
            }

            List<String> reports = SolveKnapsackProblem(itemsRatioPriceWeight, capacity);
            Console.WriteLine(string.Join("\n", reports));
        }

        private static List<string> SolveKnapsackProblem(SortedDictionary<decimal, Dictionary<decimal, decimal>> items, decimal capacity)
        {
            List<string> reports = new List<string>();
            decimal totalPrice = 0m;
            foreach (var ratio in items.Keys)
            {
                foreach (var weight in items[ratio].Keys)
                {
                    decimal percent = 1;
                    if (weight > capacity)
                    {
                        percent = capacity / weight;
                        capacity = 0;
                        totalPrice += percent * items[ratio][weight];
                    }
                    else
                    {
                        capacity -= weight;
                        totalPrice += items[ratio][weight];
                    }

                    reports.Add(
                            $"Take {percent:p2} of item with price {items[ratio][weight]:f2} and weight {weight:f2}");
                    if (capacity == 0)
                    {
                        break;
                    }
                }

                if (capacity == 0)
                {
                    break;
                }
            }

            reports.Add($"Total price: {totalPrice:f2}");

            return reports;
        }
    }
}
