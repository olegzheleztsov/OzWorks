using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sort
{
    public class QuickSorter<T> : ISorter<T>
    {
        private static readonly IRandomSource _randomSource = new DefaultRandomSource();
        
        private readonly Func<T[], Func<T, int>, Comparison<int>, int, int, int> _partition;
        
        private readonly Dictionary<PartitionStrategy, Func<T[], Func<T, int>, Comparison<int>, int, int, int>> _partitionStrategies = new Dictionary<PartitionStrategy, Func<T[], Func<T, int>, Comparison<int>, int, int, int>>()
        {
            [PartitionStrategy.Default] = PartitionDefault,
            [PartitionStrategy.OptimizeEqualArrays] = PartitionOptimizeEqualsKeys,
            [PartitionStrategy.RandomizedPartition] = PartitionRandomized
        };
        
        public QuickSorter(PartitionStrategy partitionStrategy = PartitionStrategy.Default)
        {
            _partition = ChoosePartition(partitionStrategy);
        }

        private Func<T[], Func<T, int>, Comparison<int>, int, int, int> ChoosePartition(
            PartitionStrategy partitionStrategy)
        {
            if (_partitionStrategies.TryGetValue(partitionStrategy, out var partition))
            {
                return partition;
            }
            throw new ArgumentException($"Unsupported partition: {partitionStrategy}");
        }
        
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
        {
            SortImplementation(array, keySelector, comparison, 0, array.Length - 1);
        }

        private void SortImplementation(T[] array, Func<T, int> keySelector, Comparison<int> comparison, int left,
            int right)
        {
            if (left < right)
            {
                var middle = _partition(array, keySelector, comparison, left, right);
                SortImplementation(array, keySelector, comparison, left, middle - 1);
                SortImplementation(array, keySelector, comparison, middle + 1, right);
            }
        }

        private static int PartitionDefault(T[] array, Func<T, int> keySelector, Comparison<int> comparison, int p, int r)
        {
            var pivot = keySelector(array[r]);
            var i = p - 1;
            for (var j = p; j < r; j++)
            {
                if (comparison(keySelector(array[j]), pivot) <= 0)
                {
                    i++;
                    Util.Exchange(ref array[i], ref array[j]);
                }
            }

            Util.Exchange(ref array[i + 1], ref array[r]);
            return (i + 1);
        }

        private static int PartitionOptimizeEqualsKeys(T[] array, Func<T, int> keySelector, Comparison<int> comparison,
            int p, int r)
        {
            var pivot = keySelector(array[r]);
            var i = p - 1;
            for (var j = p; j < r; j++)
            {
                if (comparison(keySelector(array[j]), pivot) <= 0 && (j % 2 == (p + 1) % 2))
                {
                    i++;
                    Util.Exchange(ref array[i], ref array[j]);
                }
            }
            Util.Exchange(ref array[i + 1], ref array[r]);
            return (i + 1);
        }

        private static int PartitionRandomized(T[] array, Func<T, int> keySelector, Comparison<int> comparison, int p,
            int r)
        {
            int index = _randomSource.RandomValue(p, r + 1);
            Util.Exchange(ref array[r], ref array[index]);
            return PartitionDefault(array, keySelector, comparison, p, r);
        }
    }
}