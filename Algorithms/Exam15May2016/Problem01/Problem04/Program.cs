namespace Problem04
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    class Program
    {
        private static Dictionary<int, bool> turnedNodes;
        private static Dictionary<int, Dictionary<int, int>> nodesWithChildren;
        /*private static int startingEnergy = 0;
        private static int wastedEnergyPerWaitedTurn = 0;
        private static int startNode = 0;
        private static int endNode = 0;*/
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            turnedNodes = new Dictionary<int, bool>();
            for (int i = 0; i < input.Length; i++)
            {
                int node = int.Parse(input[i].Substring(0, input[i].Length - 1));
                bool turnedOn = input[i][input[i].Length - 1] == 'w';
                turnedNodes.Add(node, turnedOn);
            }

            int nodeCount = input.Length;
            int startingEnergy = int.Parse(Console.ReadLine());
            int wastedEnergyPerWaitedTurn = int.Parse(Console.ReadLine());

            int startNode = int.Parse(Console.ReadLine());
            int endNode = int.Parse(Console.ReadLine());
            int countLines = int.Parse(Console.ReadLine());
            nodesWithChildren = new Dictionary<int, Dictionary<int, int>>();
            for (int i = 0; i < countLines; i++)
            {
                input = Console.ReadLine().Split();
                int parent = int.Parse(input[0]);
                int child = int.Parse(input[1]);
                int distance = int.Parse(input[2]);

                if(!nodesWithChildren.ContainsKey(parent))
                {
                    nodesWithChildren.Add(parent, new Dictionary<int, int>());
                }

                nodesWithChildren[parent].Add(child, distance);
            }

            int[] energy = new int[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                energy[i] = int.MaxValue;
            }

            energy[startNode] = 0;
            var used = new bool[nodeCount];
            int turns = 0;
            startingEnergy += wastedEnergyPerWaitedTurn;
            while (true)
            {
                int minEnergy = int.MaxValue;
                int minNode = 0;
                for (int node = 0; node < nodeCount; node++)
                {
                    if (!used[node] && energy[node] < minEnergy - wastedEnergyPerWaitedTurn &&
                        (!turnedNodes[node] && turns % 2 == 0) || (turnedNodes[node] && turns % 2 == 1))
                    {
                        minEnergy = energy[node];
                        minNode = node;
                    }

                    if (!used[node] && energy[node] < minEnergy && (turnedNodes[node] && turns % 2 == 0) || 
                        (!turnedNodes[node] && turns % 2 == 1))
                    {
                        minEnergy = energy[node];
                        minNode = node;
                    }


                }

                if (minEnergy == int.MaxValue)
                {
                    break;
                }

                used[minNode] = true;
                if (!turnedNodes[minNode] && turns % 2 == 0 && turns > 0)
                {
                    startingEnergy -= wastedEnergyPerWaitedTurn;
                    turns += 2;
                }
                else
                {
                    turns++;
                }

                if (minNode == endNode)
                {
                    break;
                }

                for (int i = 0; i < nodeCount; i++)
                {
                    if (nodesWithChildren[minNode].ContainsKey(i))
                    {
                        int newEnergy = energy[minNode] + nodesWithChildren[minNode][i];
                        if (newEnergy < energy[i])
                        {
                            energy[i] = newEnergy;
                        }
                    }
                }              
            }

            startingEnergy -= energy[endNode];

            if (startingEnergy < 0)
            {
                Console.WriteLine("Busted - need {0} more energy", 0 - startingEnergy);
                return;
            }

            Console.WriteLine(startingEnergy);
        }
    }
}
