namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Sortable_Collection.Contracts;

    public class HeapSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            this.HeapSort(collection);
        }

        private void SiftDown(List<T> collection, int start, int end)
        {
            int root = start;
            while (root * 2 + 1 <= end)
            {
                T swap = collection[root];
                int child = root * 2 + 1;
                if (child + 1 <= end && collection[child].CompareTo(collection[child + 1]) < 0)
                {
                    child++;
                }

                if (collection[root].CompareTo(collection[child]) < 0)
                {                    
                    this.Swap(collection, child, root);
                }

                if (swap.Equals(collection[root]))
                {
                    return;
                }

                root = child;
            }
        }

        private void Heapify(List<T> collection, int count)
        {
            int start = count - 1;
            if (start % 2 == 0)
            {
                start = start / 2 - 1;
            }
            else
            {
                start = start / 2;
            }

            while (start >= 0)
            {
                this.SiftDown(collection, start, count - 1);
                start--;
            }
        }

        private void HeapSort(List<T> collection)
        {
            int count = collection.Count;
            this.Heapify(collection, count);
            int end = count - 1;
            while (end > 0)
            {
                this.Swap(collection, 0, end);
                end--;
                this.SiftDown(collection, 0, end);
            }
        }

        private void Swap(List<T> collection, int indexBigger, int indexSmaller)
        {
            T old = collection[indexBigger];
            collection[indexBigger] = collection[indexSmaller];
            collection[indexSmaller] = old;
        }
    }
}
