namespace Sortable_Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class SortableCollection<T> where T : IComparable<T>
    {
        public SortableCollection()
        {
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.Items = new List<T>(items);
        }

        public SortableCollection(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public List<T> Items { get; } = new List<T>();

        public int Count => this.Items.Count;

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.Items);
        }

        public int BinarySearch(T item)
        {
            return this.BinarySearchProcedure(item, 0, this.Count - 1);
        }

        public int InterpolationSearch(List<int> collection, int key)
        {
            return this.InterpolationSearchProcedure(collection, key);
        }

        public void Shuffle()
        {
            this.FisherYatesShuffle();
        }

        public T[] ToArray()
        {
            return this.Items.ToArray();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", this.Items)}]";
        }

        private void FisherYatesShuffle()
        {
            Random random = new Random();
            for (int i = 0; i < this.Count - 1; i++)
            {
                int j = random.Next(i); 

                T tmp = this.Items[j];
                this.Items[j] = this.Items[i + 1];
                this.Items[i + 1] = tmp;
            }
        }

        private int BinarySearchProcedure(T item, int startIndex, int endIndex)
        {
            if (endIndex < startIndex)
            {
                return -1;
            }

            int midpoint = (endIndex + startIndex) / 2;
            if (item.CompareTo(this.Items[midpoint]) < 0)
            {
                endIndex = midpoint - 1;
                return this.BinarySearchProcedure(item, startIndex, endIndex);
            }

            if (item.CompareTo(this.Items[midpoint]) > 0)
            {
                startIndex = midpoint + 1;
                return this.BinarySearchProcedure(item, startIndex, endIndex);
            }

            return midpoint;
        }

        private int InterpolationSearchProcedure(List<int> collection, int key)
        {
            int low = 0;
            int high = this.Count - 1;
            if (low > high)
            {
                return -1;
            }

            while (collection[low] <= key && collection[high] >= key)
            {
                int mid = low + (key - collection[low]) * (high - low) / (collection[high] - collection[low]);
                if (collection[mid] < key)
                {
                    low = mid + 1;
                }
                else if (collection[mid] > key)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            if (collection[low] == key)
            {                
                return low;
            }

            return -1;
        }
    }
}