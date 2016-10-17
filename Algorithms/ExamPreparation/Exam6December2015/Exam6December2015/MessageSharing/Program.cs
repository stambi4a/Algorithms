namespace MessageSharing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Program
    {
        private static Dictionary<string, HashSet<string>> peopleWithFriends;
        private static HashSet<string> people;
        private static Dictionary<string, bool> visited;
        private static Queue<string> visitedQueue;
        private static int countSteps = 0;
        private static void Main(string[] args)
        {
            peopleWithFriends = new Dictionary<string, HashSet<string>>();
            people = new HashSet<string>();
            visited = new Dictionary<string, bool>();
            visitedQueue = new Queue<string>();

            string pattern = "(?<=\\s)([A-Za-z0-9]+)(,|$)";
            string input = Console.ReadLine();
            MatchCollection nameMatches = Regex.Matches(input, pattern);
            foreach (Match match in nameMatches)
            {
                GroupCollection groups = match.Groups;
                string name = groups[1].Value;
                people.Add(name);
            }

            foreach (var person in people)
            {
                visited.Add(person, false);
            }

            pattern = "(?<=\\s)([A-Za-z0-9]+) - ([A-Za-z0-9]+)(,|$)";
            input = Console.ReadLine();
            nameMatches = Regex.Matches(input, pattern);
            foreach (Match match in nameMatches)
            {
                GroupCollection groups = match.Groups;
                string personA = groups[1].Value;
                string personB = groups[2].Value;
                if(!peopleWithFriends.ContainsKey(personA))
                {
                    peopleWithFriends.Add(personA, new HashSet<string>());
                }

                peopleWithFriends[personA].Add(personB);

                if (!peopleWithFriends.ContainsKey(personB))
                {
                    peopleWithFriends.Add(personB, new HashSet<string>());
                }

                peopleWithFriends[personB].Add(personA);
            }

            var messageReceivers = new List<string>();
            pattern = "(?<=\\s)([A-Za-z0-9]+)(,|$)";
            input = Console.ReadLine();
            nameMatches = Regex.Matches(input, pattern);
            foreach (Match match in nameMatches)
            {
                GroupCollection groups = match.Groups;
                string receiver = groups[1].Value;
                messageReceivers.Add(receiver);
            }

            List<string> lastStepReceivers = BfsMessageReachPeople(messageReceivers);
            lastStepReceivers.Sort();
            var notReached = visited.Where(x => !x.Value).Select(x => x.Key).ToList();
            if (notReached.Any())
            {
                notReached.Sort();
                Console.WriteLine("Cannot reach: {0}", string.Join(", ", notReached));
            }
            else
            {
                Console.WriteLine($"All people reached in {countSteps} steps");
                Console.WriteLine($"People at last step: {string.Join(", ", lastStepReceivers)}");
            }
            
        }

        private static List<string> BfsMessageReachPeople(List<string> messageReceivers)
        {
            var stepReceivers = new List<string>();
            foreach (var messageReceiver in messageReceivers)
            {
                visitedQueue.Enqueue(messageReceiver);
                visited[messageReceiver] = true;
            }

            var lastStepReceivers = new List<string>();
            lastStepReceivers.AddRange(messageReceivers);
            while (visitedQueue.Count > 0)
            {
                stepReceivers = new List<string>();
                while (visitedQueue.Count > 0)
                {
                    var receiver = visitedQueue.Dequeue();
                    if (!peopleWithFriends.ContainsKey(receiver))
                    {
                        continue;
                    }

                    foreach (var child in peopleWithFriends[receiver])
                    {
                        if (visited[child])
                        {
                            continue;
                        }

                        stepReceivers.Add(child);
                        visited[child] = true;
                    }
                }

                if (stepReceivers.Count > 0)
                {
                    lastStepReceivers = new List<string>();
                    lastStepReceivers.AddRange(stepReceivers);
                }
                
                foreach (var child in stepReceivers)
                {
                    visitedQueue.Enqueue(child);
                }

                countSteps++;
            }

            countSteps--;
            return lastStepReceivers; ;
        }
    }
}
