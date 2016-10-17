namespace Problem_1.Fractional_Knapsack_Problem
{
    using System.Collections.Generic;

    internal class DecimalComparer : IComparer<decimal>
    {
        public int Compare(decimal o1, decimal o2)
        {
            if (o2 == o1)
            {
                return 0;
            }

            return o2 > o1 ? 1 : -1;
        }
    }
}
