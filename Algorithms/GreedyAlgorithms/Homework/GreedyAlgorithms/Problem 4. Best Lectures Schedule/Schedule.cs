namespace Problem_4.Best_Lectures_Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Schedule
    {
        private static void Main(string[] args)
        {
            var lecturesWithStartEndHour =  new SortedDictionary<int, SortedDictionary<int, string>>();
            string[] input = Console.ReadLine().Split();
            int schedules = int.Parse(input[1]);
            for (int i = 0; i < schedules; i++)
            {
                input = Console.ReadLine().Split();
                string lecture = input[0].Substring(0, input[0].Length - 1);
                int startHour = int.Parse(input[1]);
                int endHour = int.Parse(input[3]);
                if (!lecturesWithStartEndHour.ContainsKey(endHour))
                {
                    lecturesWithStartEndHour.Add(endHour, new SortedDictionary<int, string>());
                }

                lecturesWithStartEndHour[endHour].Add(startHour, lecture);
            }


            int begin = 0;
            var lectures = new List<string>();
            int lecturesCount = 0;
            foreach (var endHour in lecturesWithStartEndHour.Keys)
            {
                int startHour = lecturesWithStartEndHour[endHour].Keys.Max();
                if (startHour >= begin)
                {
                    begin = endHour;
                    string lecture = lecturesWithStartEndHour[endHour][startHour];
                    lectures.Add($"{startHour}-{endHour}"
                                 + $" -> {lecture}");
                    lecturesCount++;
                }

            }

            Console.WriteLine($"Lectures: ({lecturesCount})");
            Console.WriteLine(string.Join("\n", lectures));
        }
    }
}
