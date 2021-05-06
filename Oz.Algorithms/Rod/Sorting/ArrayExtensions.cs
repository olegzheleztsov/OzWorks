#region

using System;
using System.Collections.Generic;
using Oz.Algorithms.DataStructures;

#endregion

namespace Oz.Algorithms.Rod.Sorting
{
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Sorts array using insertion sort
        /// </summary>
        /// <param name="values">Array to sort</param>
        /// <param name="comparison">Comparision delegate</param>
        /// <typeparam name="T">Type of the array params</typeparam>
        public static void InsertionSort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            for (var i = 0; i < values.Length; i++)
            {
                var item = values[i];
                var j = i - 1;
                for (; j >= 0; j--)
                {
                    if (comparison(values[j], item) > 0)
                    {
                        values[j + 1] = values[j];
                    }
                    else
                    {
                        break;
                    }
                }

                values[j + 1] = item;
            }
        }

        /// <summary>
        ///     Sorts the array using selection sort
        /// </summary>
        /// <param name="values">Array to sort</param>
        /// <param name="comparison">Comparision delegate</param>
        /// <typeparam name="T">Type of the array params</typeparam>
        public static void SelectionSort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            for (var i = 0; i < values.Length; i++)
            {
                var smallestIndex = i;
                for (var j = i + 1; j < values.Length; j++)
                {
                    if (comparison(values[j], values[smallestIndex]) < 0)
                    {
                        smallestIndex = j;
                    }
                }

                if (smallestIndex != i)
                {
                    Util.Exchange(ref values[i], ref values[smallestIndex]);
                }
            }
        }

        /// <summary>
        ///     Sorts array with bubble sort
        /// </summary>
        /// <param name="values">Array to sort</param>
        /// <param name="comparison">Comparision delegate</param>
        /// <typeparam name="T">Type of the array params</typeparam>
        public static void BubbleSort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            var notSorted = true;
            while (notSorted)
            {
                notSorted = false;

                for (var i = 1; i < values.Length; i++)
                {
                    if (comparison(values[i], values[i - 1]) < 0)
                    {
                        Util.Exchange(ref values[i], ref values[i - 1]);
                        notSorted = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Sorts array with optimized version of bubble sort
        /// </summary>
        /// <param name="values">Array to sort</param>
        /// <param name="comparison">Comparision delegate</param>
        /// <typeparam name="T">Type of the array params</typeparam>
        public static void BubbleSortAdvanced<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            var indexMin = 0;
            var indexMax = values.Length - 1;

            while (indexMin < indexMax)
            {
                var lastSwap = indexMin;
                for (var i = indexMin; i < indexMax; i++)
                {
                    if (comparison(values[i], values[i + 1]) > 0)
                    {
                        Util.Exchange(ref values[i], ref values[i + 1]);
                        lastSwap = i;
                    }
                }

                indexMax = lastSwap;
                if (indexMin >= indexMax)
                {
                    break;
                }

                lastSwap = indexMax;
                for (var i = indexMax; i > indexMin; i--)
                {
                    if (comparison(values[i], values[i - 1]) < 0)
                    {
                        Util.Exchange(ref values[i], ref values[i - 1]);
                        lastSwap = i;
                    }
                }

                indexMin = lastSwap;
            }
        }

        public static void HeapSort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            var heap = new RodHeap<T>(values, comparison, values.Length);
            heap.HeapSort();
        }

        public static void QuicksortOnQueues<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            QuicksortOnQueuesImplementation(values, 0, values.Length - 1, comparison,
                new OzQueue<T>(values.Length), new OzQueue<T>(values.Length));

            static void QuicksortOnQueuesImplementation(IList<T> values, int start, int end, Comparison<T> comparison,
                OzQueue<T> lowerQueue, OzQueue<T> upperQueue)
            {
                if (end <= start)
                {
                    return;
                }
                
                var divider = values[start];
                for (var i = start + 1; i <= end; i++)
                {
                    if (comparison(values[i], divider) < 0)
                    {
                        lowerQueue.Enqueue(values[i]);
                    }
                    else
                    {
                        upperQueue.Enqueue(values[i]);
                    }
                }
                
                var index = start;
                while (!lowerQueue.IsEmpty)
                {
                    values[index++] = lowerQueue.Dequeue();
                }

                var midIndex = index;
                values[index++] = divider;
                while (!upperQueue.IsEmpty)
                {
                    values[index++] = upperQueue.Dequeue();
                }
                
                QuicksortOnQueuesImplementation(values, start, midIndex - 1, comparison, lowerQueue, upperQueue);
                QuicksortOnQueuesImplementation(values, midIndex + 1, end, comparison, lowerQueue, upperQueue);
            }
        }

        public static void QuicksortOnStacks<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            QuicksortOnStacksImplementation(values, 0, values.Length - 1, comparison, new OzStack<T>(values.Length),
                new OzStack<T>(values.Length));

            static void QuicksortOnStacksImplementation(IList<T> values, int start, int end, Comparison<T> comparison,
                IStack<T> lowerStack, IStack<T> upperStack)
            {
                if (end <= start)
                {
                    return;
                }

                var divider = values[start];
                for (var i = start + 1; i <= end; i++)
                {
                    if (comparison(values[i], divider) < 0)
                    {
                        lowerStack.Push(values[i]);
                    }
                    else
                    {
                        upperStack.Push(values[i]);
                    }
                }

                var index = start;
                while (!lowerStack.IsEmpty)
                {
                    values[index++] = lowerStack.Pop();
                }

                var midIndex = index;
                values[index++] = divider;
                while (!upperStack.IsEmpty)
                {
                    values[index++] = upperStack.Pop();
                }

                QuicksortOnStacksImplementation(values, start, midIndex - 1, comparison, lowerStack, upperStack);
                QuicksortOnStacksImplementation(values, midIndex + 1, end, comparison, lowerStack, upperStack);
            }
        }


        public static void Quicksort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }

            QuicksortImplementation(values, 0, values.Length - 1, comparison);

            static void QuicksortImplementation(IList<T> valuesArray, int start, int end, Comparison<T> comparisonFunc)
            {
                if (start >= end)
                {
                    return;
                }

                var divider = valuesArray[start];

                var lo = start;
                var hi = end;
                while (true)
                {
                    while (comparisonFunc(valuesArray[hi], divider) >= 0)
                    {
                        hi--;
                        if (hi <= lo)
                        {
                            break;
                        }
                    }

                    if (hi <= lo)
                    {
                        valuesArray[lo] = divider;
                        break;
                    }

                    valuesArray[lo] = valuesArray[hi];
                    lo++;
                    while (comparisonFunc(valuesArray[lo], divider) < 0)
                    {
                        lo++;
                        if (lo >= hi)
                        {
                            break;
                        }
                    }

                    if (lo >= hi)
                    {
                        lo = hi;
                        valuesArray[hi] = divider;
                        break;
                    }

                    valuesArray[hi] = valuesArray[lo];
                }

                QuicksortImplementation(valuesArray, start, lo - 1, comparisonFunc);
                QuicksortImplementation(valuesArray, lo + 1, end, comparisonFunc);
            }
        }

        public static void Mergesort<T>(this T[] values, Comparison<T> comparison)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }
            
            var scratch = new T[values.Length];
            MergesortImplementation(values, scratch, 0, values.Length - 1, comparison);

            static void MergesortImplementation(IList<T> values, IList<T> scratch, int start, int end,
                Comparison<T> comparison)
            {
                if (start >= end)
                {
                    return;
                }

                var midpoint = (start + end) / 2;
                MergesortImplementation(values, scratch, start, midpoint, comparison);
                MergesortImplementation(values, scratch, midpoint + 1, end, comparison);

                var leftIndex = start;
                var rightIndex = midpoint + 1;
                var scratchIndex = leftIndex;
                while (leftIndex <= midpoint && rightIndex <= end)
                {
                    if (comparison(values[leftIndex], values[rightIndex]) <= 0)
                    {
                        scratch[scratchIndex] = values[leftIndex];
                        leftIndex++;
                    }
                    else
                    {
                        scratch[scratchIndex] = values[rightIndex];
                        rightIndex++;
                    }

                    scratchIndex++;
                }

                for (var i = leftIndex; i <= midpoint; i++)
                {
                    scratch[scratchIndex++] = values[i];
                }

                for (var i = rightIndex; i <= end; i++)
                {
                    scratch[scratchIndex++] = values[i];
                }

                for (var i = start; i <= end; i++)
                {
                    values[i] = scratch[i];
                }
            }
        }

        public static void CountingSort(this int[] values, int maxValue)
        {
            if (!ValidateArrayForSorting(values))
            {
                return;
            }
            
            var counts = new int[maxValue + 1];

            foreach (var countIndex in values)
            {
                counts[countIndex]++;
            }

            var index = 0;
            for (var i = 0; i <= maxValue; i++)
            {
                for (var j = 1; j <= counts[i]; j++)
                {
                    values[index++] = i;
                }
            }
        }

        private static bool ValidateArrayForSorting<T>(T[] values)
        {
            if (values == null)
            {
                return false;
            }

            return values.Length >= 2;
        }
    }
}