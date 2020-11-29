using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Sort
{
    public class CountingSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
        {
            var (_, maxKey) = CheckInputs(array, keySelector);

            var computeArray = new int[maxKey + 1];
            var sortedArray = new T[array.Length];

            foreach (var element in array)
            {
                computeArray[keySelector(element)]++;
            }
            
            for (var i = 1; i < computeArray.Length; i++)
            {
                computeArray[i] += computeArray[i - 1];
            }
            
            for (var j = array.Length - 1; j >= 0; j--)
            {
                sortedArray[computeArray[keySelector(array[j])] - 1] = array[j];
                computeArray[keySelector(array[j])]--;
            }

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = sortedArray[i];
            }
        }
        

        private (int minKey, int maxKey) CheckInputs(IReadOnlyCollection<T> array, Func<T, int> keySelector)
        {
            var maxKey = array.Max(keySelector);
            if (maxKey > array.Count)
            {
                throw new ArgumentException(
                    $"Max key greater than array length, max key: {maxKey}, array length: {array.Count}");
            }

            var minKey = array.Min(keySelector);
            if (minKey < 0)
            {
                throw new ArgumentException($"Min key less than zero, min key: {minKey}, array length: {array.Count}");
            }

            return (minKey, maxKey);
        }
    }
}