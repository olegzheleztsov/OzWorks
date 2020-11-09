using System;

namespace Oz.Algorithms.Sort
{
    public interface ISorter<T> 
    {
        void Sort(T[] array, Func<T, int> keySelector, Comparison<int> comparison);
    }
}