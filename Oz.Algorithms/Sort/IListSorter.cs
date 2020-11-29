using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Sort
{
    public interface IListSorter<T>
    {
        void Sort(List<T> list, Func<T, int> keySelector, Comparison<int> comparison);
    }
}