using System;

namespace Oz.Algorithms
{
    public static class Comparisions
    {
        public static readonly Comparison<int> StandardComparision = (a, b) => a.CompareTo(b);
        public static readonly Comparison<int> DecreaseComparison = (a, b) => b.CompareTo(a);
    }
}