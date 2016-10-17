namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            T[] temporaryArray = new T[collection.Count];
            this.MergeSort(collection, temporaryArray, 0, collection.Count - 1);
        }

        private void MergeSort(List<T> array, T[] temporaryArray, int start, int end)
        {
            if (start < end)
            {
                int middle = (start + end) / 2;
                this.MergeSort(array, temporaryArray, start, middle);
                this.MergeSort(array, temporaryArray, middle + 1, end);
                this.Merge(array, temporaryArray, start, middle, end);
            }
        }

        private void Merge(List<T> array, T[] temporaryArray, int start, int middle, int end)
        {
            int leftStartIndex = start;
            int rightStartIndex = middle + 1;
            int tempIndex = 0;
            while (leftStartIndex <= middle && rightStartIndex <= end)
            {
                if (array[leftStartIndex].CompareTo(array[rightStartIndex]) <= 0)
                {
                    temporaryArray[tempIndex] = array[leftStartIndex];
                    leftStartIndex++;
                }
                else
                {
                    temporaryArray[tempIndex] = array[rightStartIndex];
                    rightStartIndex++;
                }

                tempIndex++;
            }

            while (leftStartIndex <= middle)
            {
                temporaryArray[tempIndex] = array[leftStartIndex];
                leftStartIndex++;
                tempIndex++;
            }

            while (rightStartIndex <= end)
            {
                temporaryArray[tempIndex] = array[rightStartIndex];
                rightStartIndex++;
                tempIndex++;
            }

            tempIndex = 0;
            leftStartIndex = start;
            while (tempIndex < temporaryArray.Length && leftStartIndex <= end)
            {
                array[leftStartIndex] = temporaryArray[tempIndex];
                tempIndex++;
                leftStartIndex++;
            }
        }
    }
}
