#region

using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Recursion;

public class ClimbStairsSolver
{
    public static int ClimbStairs(int n)
    {
        var cache = new Dictionary<int, int>();
        return ClimbStairsImpl(n, cache);
    }

    private static int ClimbStairsImpl(int n, IDictionary<int, int> cache)
    {
        if (cache.ContainsKey(n))
        {
            return cache[n];
        }

        switch (n)
        {
            case 1:
                return 1;
            case 2:
                return 2;
        }

        var result = ClimbStairsImpl(n - 1, cache) + ClimbStairsImpl(n - 2, cache);
        cache[n] = result;
        return result;
    }
}