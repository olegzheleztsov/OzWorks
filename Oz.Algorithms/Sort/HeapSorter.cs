using System;
using Oz.Algorithms.DataStructures;

namespace Oz.Algorithms.Sort
{
    public class HeapSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
        {
            var heap = Heap<T>.MaxHeap(array, keySelector, comparison);
            for (var i = array.Length - 1; i > 0; i--)
            {
                var temp = heap[0];
                heap[0] = heap[i];
                heap[i] = temp;
                heap.HeapSize--;
                heap.MaxHeapify(0);
            }
        }
    }
}