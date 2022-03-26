// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet532
{
    public int FindPairs(int[] nums, int k)
    {
        var count = 0;
        var map = new Dictionary<int, int>();

        foreach (var num in nums)
        {
            if (!map.ContainsKey(num))
            {
                map.Add(num, 1);
            }
            else
            {
                map[num]++;
            }
        }

        foreach (var key in map.Keys)
        {
            if (k == 0)
            {
                if (map[key] > 1)
                {
                    count++;
                }
            }
            else
            {
                if (map.ContainsKey(key + k))
                {
                    count++;
                }
            }
        }
        return count;
    }
}