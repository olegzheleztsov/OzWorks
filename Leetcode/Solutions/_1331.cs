// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
Given an array of integers arr, replace each element with its rank.

The rank represents how large the element is. The rank has the following rules:

Rank is an integer starting from 1.
The larger the element, the larger the rank. If two elements are equal, their rank must be the same.
Rank should be as small as possible.
 */
public class _1331
{
    public int[] ArrayRankTransform(int[] arr)
    {
        var ranks = new Dictionary<int, int>();
        var nextRank = 1;
        var sortedArray = arr.OrderBy(a => a).ToArray();
        foreach (var num in sortedArray)
        {
            if (!ranks.ContainsKey(num))
            {
                ranks[num] = nextRank++;
            }
        }

        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = ranks[arr[i]];
        }

        return arr;
    }
}