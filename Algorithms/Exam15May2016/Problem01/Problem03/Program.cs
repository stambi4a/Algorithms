namespace Problem03
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static Dictionary<int, HashSet<int>> graph;
        private static Dictionary<int, int> predecessorCount;
        static void Main(string[] args)
        {
            graph = new Dictionary<int, HashSet<int>>();
            int stickNumbers = int.Parse(Console.ReadLine());
            int placings = int.Parse(Console.ReadLine());
            for (int i = 0; i < placings; i++)
            {
                string[] input = Console.ReadLine().Split();
                int top = int.Parse(input[0]);
                int under = int.Parse(input[1]);
                if(!graph.ContainsKey(top))
                {
                    graph.Add(top, new HashSet<int>());
                }

                if (graph.ContainsKey(under) && graph[under].Contains(top))
                {
                    continue;
                }

                graph[top].Add(under);
            }

            predecessorCount = new Dictionary<int, int>();

            for (int i = 0; i < stickNumbers; i++)
            {
                predecessorCount.Add(i, 0);
            }

            foreach (var stick in graph.Keys)
            {
                foreach (var under in graph[stick])
                {
                    predecessorCount[under]++;
                }
            }

            var sticks = new List<int>();
            while (true)
            {
                bool stickIsRemoved = false;
                var removedSticks = new List<int>();
                foreach (var stick in predecessorCount.Keys)
                {
                    if (predecessorCount[stick] == 0)
                    {
                        stickIsRemoved = true;
                        removedSticks.Add(stick);
                    }
                }

                if (!stickIsRemoved)
                {
                    break;
                }

                removedSticks.Sort();
                int biggestStick = removedSticks[removedSticks.Count - 1];
                if (graph.ContainsKey(biggestStick))
                {
                    foreach (var under in graph[biggestStick])
                    {
                        predecessorCount[under]--;
                    }
                }

                predecessorCount.Remove(biggestStick);

                sticks.Add(biggestStick);

                               
            }

            if (predecessorCount.Count > 0)
            {
                Console.WriteLine("Cannot lift all sticks");
            }

            Console.WriteLine(string.Join(" ", sticks));
        }
    }
}
