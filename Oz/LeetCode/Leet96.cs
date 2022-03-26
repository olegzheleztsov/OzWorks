// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet96
{
    public int NumTrees(int n)
    {
        var memo = new Dictionary<int, int> {[0] = 1, [1] = 1, [2] = 2};
        return NumTreesInner(n, memo);
    }

    private int NumTreesInner(int n, Dictionary<int, int> memo)
    {
        if (n < 0)
        {
            return 1;
        }

        if (memo.ContainsKey(n))
        {
            return memo[n];
        }

        var total = 0;
        for (var i = 0; i <= n; i++)
        {
            total += NumTreesInner(i, memo) * NumTreesInner(n - i - 1, memo);
        }

        memo[n] = total;
        return total;
    }
}