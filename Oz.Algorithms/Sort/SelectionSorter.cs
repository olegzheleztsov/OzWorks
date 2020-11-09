using System;

namespace Oz.Algorithms.Sort
{
    public class SelectionSorter<T> : ISorter<T>
    {
        public void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison)
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
                    if (comparison(keySelector(array[j]), keySelector(array[minIndex])) < 0)
                    {
                        minIndex = j;
                    }
                }

                var temp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = temp;
            }
        }
        
    }
}