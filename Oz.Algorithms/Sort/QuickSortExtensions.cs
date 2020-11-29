using System;

namespace Oz.Algorithms.Sort
{
    public static class QuickSortExtensions
    {
        private static readonly IRandomSource RandomSource = new DefaultRandomSource();
        
        /// <summary>
        /// Partition array in two parts. All elements left from result pivot index less or equal than pivot element, all elements
        /// right to pivot index greater than pivot element
        /// </summary>
        /// <param name="array">Array to be partitioned</param>
        /// <param name="keySelector">Key selector for partition</param>
        /// <param name="comparison">Comparision for keys</param>
        /// <param name="lowBoundIndex">Low bound index</param>
        /// <param name="upperBoundIndex">Upper bound index</param>
        /// <typeparam name="T">Type of the array elements</typeparam>
        /// <returns>Partitioning index</returns>
        public static int PartitionDefault<T>(this T[] array, Func<T, int> keySelector, Comparison<int> comparison,
            int lowBoundIndex, int upperBoundIndex)
        {
            var pivot = keySelector(array[upperBoundIndex]);
            var i = lowBoundIndex - 1;
            for (var j = lowBoundIndex; j < upperBoundIndex; j++)
            {
                if (comparison(keySelector(array[j]), pivot) <= 0)
                {
                    i++;
                    Util.Exchange(ref array[i], ref array[j]);
                }
            }

            Util.Exchange(ref array[i + 1], ref array[upperBoundIndex]);
            return (i + 1);
        }

        /// <summary>
        /// Partition subarray on two parts using random element as pivot index
        /// </summary>
        /// <param name="array">Array to be partitioned</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="comparison">Comparision of keys</param>
        /// <param name="lowerBoundIndex">Lower index of subarray</param>
        /// <param name="upperBoundIndex">Upper index of subarray</param>
        /// <typeparam name="T">Type of array's elements</typeparam>
        /// <returns>Pivot index of partitioning</returns>
        public static int RandomizedPartition<T>(this T[] array, Func<T, int> keySelector, Comparison<int> comparison,
            int lowerBoundIndex, int upperBoundIndex)
        {
            var index = RandomSource.RandomValue(lowerBoundIndex, upperBoundIndex + 1);
            Util.Exchange(ref array[upperBoundIndex], ref array[index]);
            return array.PartitionDefault(keySelector, comparison, lowerBoundIndex, upperBoundIndex);
        }
    }
}