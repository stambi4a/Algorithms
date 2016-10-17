namespace Sortable_Collection
{
    using System;

    using Sortable_Collection.Sorters;

    public class SortableCollectionPlayground
    {
        private static Random Random = new Random();

        public static void Main(string[] args)
        {
            const int NumberOfElementsToSort = 100;
            const int MaxValue = 999;

            var array = new int[NumberOfElementsToSort];

            for (int i = 0; i < NumberOfElementsToSort; i++)
            {
                array[i] = Random.Next(MaxValue);
            }

            var collectionToSort = new SortableCollection<int>(array);
            collectionToSort.Sort(new BucketSorter {Max = MaxValue});

            Console.WriteLine(collectionToSort);
            Console.WriteLine();

            var collection = new SortableCollection<int>(2, -1, 5, 0, -3);
            Console.WriteLine(collection);

            collection.Sort(new Quicksorter<int>());
            Console.WriteLine(collection);
            Console.WriteLine();

            var mergeCollection = new SortableCollection<int>(10, -39, 19, 0, -16, 7, 9, 12, -4);
            Console.WriteLine(mergeCollection);

            mergeCollection.Sort(new MergeSorter<int>());
            Console.WriteLine(mergeCollection);
            Console.WriteLine();

            var heapCollection = new SortableCollection<int>(10, -39, 19, 0, -16, 7, 9, 12, -4);
            Console.WriteLine(heapCollection);

            heapCollection.Sort(new HeapSorter<int>());
            Console.WriteLine(heapCollection);
            Console.WriteLine();

            var unshuffledCollection = new SortableCollection<int>(10, 9, -8, 6, 13, -25, 7, 4, 1, 0);
            Console.WriteLine(unshuffledCollection);
           
            for (int i = 1; i <= 10; i++)
            {
                unshuffledCollection.Shuffle();
                Console.WriteLine(unshuffledCollection);
            }           
        }
    }
}
