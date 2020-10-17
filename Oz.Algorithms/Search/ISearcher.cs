using System;

namespace Oz.Algorithms.Search
{
    public interface ISearcher<T>
    {
        int? FindIndex(T[] array, int searchKey, Func<T, int> keySelector);
    }
}