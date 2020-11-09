using System;

namespace Oz.Algorithms.Sort
{
    public class InsertionSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
        {
            if (array.Length == 0)
            {
                return;
            }

            for (var j = 1; j < array.Length; j++)
            {
                var keyElement = array[j];
                var i = j - 1;
                while (i >= 0 && comparison(keySelector(keyElement), keySelector(array[i])) < 0)
                {
                    array[i + 1] = array[i];
                    i--;
                }

                array[i + 1] = keyElement;
            }
        }
    }
}