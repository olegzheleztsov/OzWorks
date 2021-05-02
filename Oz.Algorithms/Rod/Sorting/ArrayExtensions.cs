using System;

namespace Oz.Algorithms.Rod.Sorting
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Sorts array using insertion sort
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
        /// Sorts the array using selection sort
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
        /// Sorts array with bubble sort
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
                    if (comparison(values[i], values[i-1]) < 0)
                    {
                        Util.Exchange(ref values[i], ref values[i - 1]);
                        notSorted = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts array with optimized version of bubble sort
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