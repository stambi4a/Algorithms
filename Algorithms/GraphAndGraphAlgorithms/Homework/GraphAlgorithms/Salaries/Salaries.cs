namespace Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Salaries
    {
        private static Dictionary<int, HashSet<int>> employees;
        private static Dictionary<int, int> predecessorCount;
        private static Dictionary<int, long> employeeSalaries; 

        private static void Main(string[] args)
        {
            employees = new Dictionary<int, HashSet<int>>();
            predecessorCount = new Dictionary<int, int>();
            int countWorkers = int.Parse(Console.ReadLine());
            employeeSalaries = new Dictionary<int, long>();
            CheckAllEmployeeEmployeeRelations(countWorkers);
            FindPredecessorCountForEmployees(countWorkers);
            for (int i = 0; i < countWorkers; i++)
            {
                employeeSalaries.Add(i, 0);
            }

            CalculateSalaries();

            long salariesSum = 0;
            employeeSalaries.Values.ToList().ForEach( x => salariesSum += x);
            Console.WriteLine(salariesSum);
        }

        private static void CalculateSalaries()
        {
            var bosses = predecessorCount.Keys.Where(x => predecessorCount[x] == 0);
            foreach (var boss in bosses)
            {
                employeeSalaries[boss] = DfsEmployees(boss);
            }
        }

        private static long DfsEmployees(int manager)
        {
            if (employees[manager].Count == 0)
            {
                return 1;
            }

            foreach (var managed in employees[manager])
            {
                if (employeeSalaries[managed] == 0)
                {
                    employeeSalaries[managed] = DfsEmployees(managed);
                }
               
                employeeSalaries[manager] += employeeSalaries[managed];
            }

            return employeeSalaries[manager];
        }

        private static void FindPredecessorCountForEmployees(int countWorkers)
        {
            for (int i = 0; i < countWorkers; i++)
            {
                if (!predecessorCount.ContainsKey(i))
                {
                    predecessorCount.Add(i, 0);
                }

                foreach (var managed in employees[i])
                {
                    if (!predecessorCount.ContainsKey(managed))
                    {
                        predecessorCount.Add(managed, 0);
                    }

                    predecessorCount[managed]++;
                }
            }
        }

        private static void CheckAllEmployeeEmployeeRelations(int countWorkers)
        {
            for (int i = 0; i < countWorkers; i++)
            {
                string input = Console.ReadLine();
                int length = input.Length;
                employees.Add(i, new HashSet<int>());
                for (int j = 0; j < length; j++)
                {
                    if (input[j] == 'Y')
                    {
                        employees[i].Add(j);
                    }
                }
            }
        }


    }
}
