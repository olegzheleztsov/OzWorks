// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1894
{
    public int ChalkReplacer(int[] chalk, int k)
    {
        var prefixSum = new long[chalk.Length];
        long cur = 0;
        for (var i = 0; i < chalk.Length; i++)
        {
            cur += chalk[i];
            prefixSum[i] = cur;
        }

        var index = Array.BinarySearch(prefixSum, k % cur);
        return index >= 0 ? index + 1 : ~index;
    }
}