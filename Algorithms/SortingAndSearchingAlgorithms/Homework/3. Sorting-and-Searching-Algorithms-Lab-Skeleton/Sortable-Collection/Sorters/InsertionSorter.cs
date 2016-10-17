namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class InsertionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            for (int i = 1; i <= collection.Count - 1; i ++)
            {
                int j = i;
                while (j > 0 && collection[j - 1].CompareTo(collection[j]) > 0)
                {
                    collection.Insert(j - 1, collection[j]);
                    collection.RemoveAt(j + 1);
                    j--;
                }
            }
        }
    }
}
