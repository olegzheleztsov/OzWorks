#region

using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Recursion;

public class KthGrammarSolver
{
    public static int KthGrammar(int n, int k) =>
        KthGrammarImpl(n, k, new Dictionary<int, Dictionary<int, int>>());

    private static int KthGrammarImpl(int n, int k, Dictionary<int, Dictionary<int, int>> cache)
    {
        if (n == 1 && k == 1)
        {
            return 0;
        }

        if (cache.ContainsKey(n))
        {
            var kDict = cache[n];
            if (kDict.ContainsKey(k))
            {
                return kDict[k];
            }
        }

        int result;
        if (k % 2 != 0)
        {
            result = KthGrammarImpl(n - 1, (k / 2) + 1, cache);
        }
        else
        {
            result = KthGrammarImpl(n - 1, k / 2, cache);
            result = result == 1 ? 0 : 1;
        }

        if (cache.ContainsKey(n))
        {
            cache[n].Add(k, result);
        }
        else
        {
            var dict = new Dictionary<int, int>();
            cache.Add(n, dict);
            dict.Add(k, result);
        }

        return result;
    }
}