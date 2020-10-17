using System;

namespace Oz.Algorithms.Sort
{
    public class SelectionSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, SortDirection direction = SortDirection.Ascending)
        {
            if (array.Length == 0)
            {
                return;
            }

            for (var i = 0; i < array.Length - 1; i++)
            {
                var minIndex = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (keySelector(array[j]).CompareTo(keySelector(array[minIndex])) == ComparisionSign(direction))
                    {
                        minIndex = j;
                    }
                }

                var temp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = temp;
            }
        }

        private int ComparisionSign(SortDirection direction)
        {
            return direction == SortDirection.Ascending ? -1 : 1;
        }
    }
}