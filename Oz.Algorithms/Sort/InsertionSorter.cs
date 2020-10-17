using System;

namespace Oz.Algorithms.Sort
{
    public class InsertionSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, SortDirection direction = SortDirection.Ascending)
        {
            if (array.Length == 0)
            {
                return;
            }

            for (var j = 1; j < array.Length; j++)
            {
                var keyElement = array[j];
                var i = j - 1;
                while (i >= 0 && keySelector(keyElement).CompareTo(keySelector(array[i])) == ComparisionSign(direction))
                {
                    array[i + 1] = array[i];
                    i--;
                }

                array[i + 1] = keyElement;
            }
        }

        private int ComparisionSign(SortDirection direction)
            => direction == SortDirection.Ascending ? -1 : 1;
    }
}