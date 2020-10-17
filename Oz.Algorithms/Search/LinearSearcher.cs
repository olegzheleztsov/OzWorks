using System;

namespace Oz.Algorithms.Search
{
    public class LinearSearcher<T> : ISearcher<T> 
    {
        public int? FindIndex(T[] array, int searchKey, Func<T, int> keySelector)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            for (var i = 0; i < array.Length; i++)
            {
                if (keySelector(array[i]).CompareTo(searchKey) == 0)
                {
                    return i;
                }
            }

            return null;
        }
    }
}