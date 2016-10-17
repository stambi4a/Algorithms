namespace Problem_5.Egyptian_Fractions
{
    using System;
    using System.Collections.Generic;

    internal class EgyptianFractions
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                string line = Console.ReadLine();
                if(string.IsNullOrEmpty(line))
                {
                    break;
                }
                string[] input = line.Split('/');
                long p = long.Parse(input[0]);
                long q = long.Parse(input[1]);
                long nominator = p;
                long denominator = q;
                if (nominator >= denominator)
                {
                    Console.WriteLine("Error (fraction is equal to or greater than 1)");
                }

                var fractions = new List<string>();
                while (nominator != 0)
                {
                    long old = denominator;
                    denominator = (long)Math.Ceiling(denominator / (double)nominator);
                    fractions.Add("1/" + denominator);
                    nominator = denominator * nominator - old;
                    denominator *= old;
                    if (denominator == long.MinValue)
                    {
                        Console.WriteLine("{0} / {1} is not Egyptian fraction.", p, q);
                        return;
                    }
                }

                Console.WriteLine($"{p}/{q} = {string.Join(" + ", fractions)}");

                
            }
            

        }
    }
}
