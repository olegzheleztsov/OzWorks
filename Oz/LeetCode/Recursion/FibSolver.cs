using System.Collections.Generic;

namespace Oz.LeetCode.Recursion
{
    public class FibSolver
    {
        public int Fib(int n)
        {
            var cache = new Dictionary<int, int>();
            return FibInner(n, cache);
        }

        private int FibInner(int n, Dictionary<int, int> cache)
        {
            if (cache.ContainsKey(n))
            {
                return cache[n];
            }

            if (n < 2)
            {
                return n;
            }

            var result = FibInner(n - 1, cache) + FibInner(n - 2, cache);
            cache[n] = result;
            return result;
        }
    }
}