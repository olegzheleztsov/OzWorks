// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet939
{
    public int MinAreaRect(int[][] points)
    {
        int minArea = int.MaxValue;
        Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();

        foreach (int[] point in points)
        {
            if (map.ContainsKey(point[0]))
            {
                map[point[0]].Add(point[1]);
            }
            else
            {
                map.Add(point[0], new HashSet<int>(){point[1]});
            }
        }

        foreach (int[] point0 in points)
        {
            foreach (int[] point1 in points)
            {
                if (point0[0] != point1[0] && point0[1] != point1[1] && map[point0[0]].Contains(point1[1]) &&
                    map[point1[0]].Contains(point0[1]))
                {
                    minArea = Math.Min(minArea, Math.Abs((point1[0] - point0[0]) * (point1[1] - point0[1])));
                }
            }
        }

        return minArea == int.MaxValue ? 0 : minArea;
    }
}