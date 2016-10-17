namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 923;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            var sortedCoins = coins.OrderByDescending(c => c).ToList();
            var chosenCoins = new Dictionary<int, int>();
            var currentSum = 0;
            var coinIndex = 0;
            while (currentSum < targetSum && coinIndex < sortedCoins.Count)
            {
                int countCoins = (targetSum - currentSum) / sortedCoins[coinIndex];
                if (countCoins > 0)
                {
                    chosenCoins.Add(sortedCoins[coinIndex], countCoins);
                    currentSum += countCoins * sortedCoins[coinIndex];
                }

                coinIndex++;
            }

            if (currentSum != targetSum)
            {
                throw new InvalidOperationException(
                    "Greedy algorithm can not produce desired sum with specified coins.");
            }

            return chosenCoins;
        }
    }
}