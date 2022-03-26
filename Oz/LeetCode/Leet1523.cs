// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1523
{
    public int CountOdds(int low, int high)
    {
        int min = Math.Min(low, high);
        int max = Math.Max(low, high);

        if (min % 2 == 0 && max % 2 == 0)
        {
            return (max - min) / 2;
        }

        return (max - min) / 2 + 1;
    }
}