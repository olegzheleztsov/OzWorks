﻿// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _436
{
    public int[] FindRightInterval(int[][] intervals)
    {
        int n = intervals.Length;
        var result = new int[n];
        var startTimeAndIndex = new List<int[]>();

        for (int i = 0; i < n; i++)
        {
            startTimeAndIndex.Add(new int[]{intervals[i][0], i});
        }
        startTimeAndIndex.Sort((x, y) => x[0].CompareTo(y[0]));
        var startTimes = startTimeAndIndex.Select(x => x[0]).ToList();
        for (int i = 0; i < n; i++)
        {
            var index = startTimes.BinarySearch(intervals[i][1]);
            if (index < 0)
            {
                index = ~index;
            }

            result[i] = index == n ? -1 : startTimeAndIndex[index][1];
        }

        return result;
    }
}