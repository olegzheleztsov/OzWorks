using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Rod.Search
{
    public static class ArrayExtensions
    {
        public static int? LinearSearch<T>(this T[] array, T value, Comparison<T> comparison)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            for (var i = 0; i < array.Length; i++)
            {
                if (comparison(array[i], value) == 0)
                {
                    return i;
                }
            }

            return null;
        }

        public static int? LinearSearchRecursive<T>(this T[] array, T value, Comparison<T> comparison)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            return LinearSearchRecursiveImplementation(array, value, comparison, 0);
            
            int? LinearSearchRecursiveImplementation(T[] arrayInner, T valueInner, Comparison<T> comparisonInner, int startIndex)
            {
                if (startIndex >= arrayInner.Length)
                {
                    return null;
                }

                return comparisonInner(arrayInner[startIndex], valueInner) == 0 ? startIndex : LinearSearchRecursiveImplementation(arrayInner, valueInner, comparisonInner, startIndex + 1);
            }
        }

        public static int? BinSearchRecursive<T>(this T[] array, T value, Comparison<T> comparison)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            return BinSearchRecursiveImplementation(array, value, comparison, 0, array.Length - 1);

            static int? BinSearchRecursiveImplementation(IReadOnlyList<T> arrayInner, T valueInner, Comparison<T> comparisonInner, int start, int end)
            {
                if (start == end)
                {
                    if (comparisonInner(arrayInner[start], valueInner) == 0)
                    {
                        return start;
                    }

                    return null;
                }
                var mid = (int) Math.Floor((start + end) / 2.0);
                if (comparisonInner(arrayInner[mid], valueInner) == 0)
                {
                    return mid;
                }

                return comparisonInner(arrayInner[mid], valueInner) < 0 
                    ? BinSearchRecursiveImplementation(arrayInner, valueInner, comparisonInner, mid + 1, end) 
                    : BinSearchRecursiveImplementation(arrayInner, valueInner, comparisonInner, start, mid - 1);
            }
        }

        public static int? BinSearch<T>(this T[] array, T value, Comparison<T> comparison)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }
            
            var start = 0;
            var end = array.Length - 1;
            

            while (start <= end)
            {
                if (start == end)
                {
                    if (comparison(array[start], value) == 0)
                    {
                        return start;
                    }

                    return null;
                }
                
                var mid = (int) Math.Floor((start + end) / 2.0);
                if (comparison(array[mid], value) == 0)
                {
                    return mid;
                }

                if (comparison(array[mid], value) < 0)
                {
                    start = mid + 1;
                } else if (comparison(array[mid], value) > 0)
                {
                    end = mid - 1;
                }
            }

            return null;
        }
    }
}