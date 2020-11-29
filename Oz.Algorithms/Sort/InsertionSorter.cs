using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sort
{
    public class InsertionSorter<T> : ISorter<T>, IListSorter<T>
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

        public void Sort(List<T> list, Func<T, int> keySelector, Comparison<int> comparison)
        {
            if (list.Count == 0)
            {
                return;
            }
            for (var j = 1; j < list.Count; j++)
            {
                var keyElement = list[j];
                var i = j - 1;
                while (i >= 0 && comparison(keySelector(keyElement), keySelector(list[i])) < 0)
                {
                    list[i + 1] = list[i];
                    i--;
                }

                list[i + 1] = keyElement;
            }
        }
    }
}