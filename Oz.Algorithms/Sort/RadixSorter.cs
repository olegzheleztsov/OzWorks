using System;

namespace Oz.Algorithms.Sort
{
    public class RadixSorter<T>
    {
        private readonly Comparison<int> _comparison;
        private readonly Func<T, int, int> _keySelector;

        public RadixSorter(Func<T, int, int> keySelector, Comparison<int> comparison)
        {
            _keySelector = keySelector;
        }

        public void Sort(T[] array, int count)
        {
            var sorter = new CountingSorter<T>();
            for (var i = 0; i < count; i++)
            {
                var index = i;

                int KeySelector(T data)
                {
                    return _keySelector(data, index);
                }

                sorter.Sort(array, KeySelector, _comparison);
            }
        }
    }
}