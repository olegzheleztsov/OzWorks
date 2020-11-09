using System;

namespace Oz.Algorithms.Sort
{
    public class IntInsertionSorter : InsertionSorter<int>
    {

        private readonly Comparison<int> _comparison;

        public IntInsertionSorter(Comparison<int> comparison = null)
        {
            _comparison = comparison ?? ((a, b) => a.CompareTo(b));
        }
        public void Sort(int[] elements)
        {
            base.Sort(elements, element => element, _comparison);
        }
    }
}