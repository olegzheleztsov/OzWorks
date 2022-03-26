// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1014
{
    public int MaxScoreSightseeingPair(int[] values)
    {
        var maxValue = 0;

        for (var i = 0; i < values.Length; i++)
        {
            for (var j = i + 1; j < values.Length; j++)
            {
                var curValue = values[i] + values[j] + i - j;
                maxValue = Math.Max(maxValue, curValue);
            }
        }

        return maxValue;
    }

    public int MaxScoreSightseeingPair2(int[] values)
    {
        var maxSums = new int[values.Length];
        maxSums[0] = values[0];

        for (var i = 1; i < values.Length; i++)
        {
            maxSums[i] = Math.Max(maxSums[i - 1], values[i] + i);
        }

        var result = int.MinValue;
        for (var j = 1; j < values.Length; j++)
        {
            result = Math.Max(result, values[j] + maxSums[j - 1] - j);
        }

        return result;
    }
}