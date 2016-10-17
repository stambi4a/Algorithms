namespace ProcessorScheduling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class ProcessorScheduling
    {
        private static void Main(string[] args)
        {
            var tasksByDeadline = new SortedDictionary<int, Dictionary<int, int>>();
            int tasks = int.Parse(Regex.Match(Console.ReadLine(), "\\d+").ToString());
            for (int i = 0; i < tasks; i++)
            {
                string[] input = Console.ReadLine().Split();
                int value = int.Parse(input[0]);
                int deadline = int.Parse(input[2]);
                if (!tasksByDeadline.ContainsKey(deadline))
                {
                    tasksByDeadline.Add(deadline, new Dictionary<int, int>());
                }

                tasksByDeadline[deadline].Add(value, i + 1);
            }

            var optimalSchedule = new List<int>();
            var keys = tasksByDeadline.Keys.ToList();
            keys.Reverse();
            var values = new List<KeyValuePair<int, int>>();
            var totalValue = 0;
            foreach (var deadline in keys)
            {
                values.AddRange(tasksByDeadline[deadline]);
                values = values.OrderBy(x=>x.Key).ToList();
                int length = values.Count;
                if (length > 0)
                {
                    int value = values[length - 1].Key;
                    optimalSchedule.Add(values[length - 1].Value);
                    totalValue += value;
                    values.RemoveAt(length - 1);
                }
            }

            optimalSchedule.Reverse();
            Console.WriteLine("Optimal schedule:  " + string.Join("-> ", optimalSchedule));
            Console.WriteLine("Total value: " + totalValue);
        }
    }
}
