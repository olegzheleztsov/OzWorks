using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Sort
{
    public class BucketSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
        {
            var maxKey = array.Select(keySelector).Max();

            float TransformedKeySelector(T element)
            {
                var sourceKey = keySelector(element);
                return sourceKey / (float) maxKey;
            }

            var bucket = new List<T>[array.Length];
            for (var i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<T>();
            }

            foreach (var element in array)
            {
                var bucketIndex = (int) Math.Floor(array.Length * TransformedKeySelector(element));
                if (bucketIndex >= bucket.Length)
                {
                    bucketIndex = bucket.Length - 1;
                }
                bucket[bucketIndex].Add(element);
            }

            var sorter = new InsertionSorter<T>();
            foreach (var buck in bucket)
            {
                sorter.Sort(buck, keySelector, comparison);
            }

            var flattened = bucket.SelectMany(buck => buck);
            var index = 0;
            foreach (var element in flattened)
            {
                array[index++] = element;
            }
        }
    }
}