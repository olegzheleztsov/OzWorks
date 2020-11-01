using System;

namespace Oz.Algorithms.Numerics
{
    public class RandomizedInterview<T> : Interview<T>
    {
        public RandomizedInterview(T[] candidates, Func<T, int> rank) : base(candidates, rank)
        {
            candidates.ShuffleInPlace();
        }
    }
}