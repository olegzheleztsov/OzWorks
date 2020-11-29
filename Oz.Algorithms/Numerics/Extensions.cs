using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Sort;

namespace Oz.Algorithms.Numerics
{
    public static class Extensions
    {
        public static int Minimum(this IEnumerable<int> collection)
        {
            return collection.Minimum(data => data, Comparisions.StandardComparision).value;
        }

        public static int Maximum(this IEnumerable<int> collection)
        {
            return collection.Maximum(data => data, Comparisions.DecreaseComparison).value;
        }

        public static float Minimum(this IEnumerable<float> collection)
        {
            return collection.Minimum(data => data, (a, b) => a.CompareTo(b)).value;
        }

        public static float Maximum(this IEnumerable<float> collection)
        {
            return collection.Maximum(data => data, (a, b) => b.CompareTo(a)).value;
        }

        public static double Minimum(this IEnumerable<double> collection)
        {
            return collection.Minimum(data => data, (a, b) => a.CompareTo(b)).value;
        }

        public static double Maximum(this IEnumerable<double> collection)
        {
            return collection.Maximum(data => data, (a, b) => b.CompareTo(a)).value;
        }

        public static (T value, int index) Maximum<T, TU>(this IEnumerable<T> collection, Func<T, TU> keySelector, Comparison<TU> comparison) 
            where TU : IComparable
        {
            return collection.MinimumImplementation(keySelector, comparison);
        }

        public static (T value, int index) Minimum<T, TU>(this IEnumerable<T> collection, Func<T, TU> keySelector, Comparison<TU> comparison)
            where TU : IComparable
        {
            return collection.MinimumImplementation(keySelector, comparison);
        }

        private static (T value, int index) MinimumImplementation<T, TU>(this IEnumerable<T> collection,
            Func<T, TU> keySelector, Comparison<TU> comparison) where TU : IComparable
        {
            var array = collection.ToArray();
            switch (array.Length)
            {
                case 0:
                    throw new ArgumentException(nameof(collection));
                case 1:
                    return (array[0], 0);
            }

            var minKey = keySelector(array[0]);
            var minValue = array[0];
            var minIndex = 0;

            for (var i = 1; i < array.Length; i++)
            {
                var currentKey = keySelector(array[i]);
                if (comparison(minKey, currentKey) > 0)
                {
                    minKey = currentKey;
                    minIndex = i;
                    minValue = array[i];
                }
            }

            return (minValue, minIndex);
        }


        public static T RandomizedSelect<T>(this T[] array, int index, Func<T, int> keySelector)
        {
            return array.RandomizedSelect(0, array.Length - 1, index, keySelector, Comparisions.StandardComparision);
        }

        private static T RandomizedSelect<T>(this T[] array, int lowerBoundIndex, int upperBoundIndex, int selectionIndex, Func<T, int> keySelector,
            Comparison<int> comparison)
        {
            if (lowerBoundIndex == upperBoundIndex)
            {
                return array[lowerBoundIndex];
            }

            var q = array.RandomizedPartition(keySelector, comparison, lowerBoundIndex, upperBoundIndex);
            var k = q - lowerBoundIndex + 1;
            if (selectionIndex == k)
            {
                return array[q];
            } else if (selectionIndex < k)
            {
                return array.RandomizedSelect(lowerBoundIndex, q - 1, selectionIndex, keySelector, comparison);
            }
            else
            {
                return array.RandomizedSelect(q + 1, upperBoundIndex, selectionIndex - k, keySelector, comparison);
            }
        }
    }
}