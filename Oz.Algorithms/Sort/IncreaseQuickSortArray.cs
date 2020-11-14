using System;

namespace Oz.Algorithms.Sort
{
    public class IncreaseQuickSortArray<T>
    {
        private readonly ISorter<T> _sorter = new QuickSorter<T>();
        private readonly T[] _array;
        private readonly Func<T, int> _keySelector;
        private bool _isSorted;
        

        public IncreaseQuickSortArray(T[] array, Func<T, int> keySelector)
        {
            _array = new T[array.Length];
            Array.Copy(array, _array, array.Length);
            _keySelector = keySelector;
            _isSorted = false;
        }

        private T[] Value
        {
            get
            {
                if (!_isSorted)
                {
                    _sorter.Sort(_array, _keySelector, Comparisions.StandardComparision);
                    _isSorted = true;
                }

                return _array;
            }
        }
        
        public static implicit operator T[](IncreaseQuickSortArray<T> source)
        {
            return source.Value;
        }
    }
}