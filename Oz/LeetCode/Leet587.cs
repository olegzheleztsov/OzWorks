// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet587
{
    public int[][] OuterTrees(int[][] trees) =>
        Jarvis(trees.ToList()).ToArray();

    private ISet<int[]> Jarvis(IList<int[]> points)
    {
        if (points.Count < 3)
        {
            return new HashSet<int[]>();
        }

        var res = new HashSet<int[]>();
        int leftPointIndex = 0;
        for (int i = 1; i < points.Count; i++)
        {
            if (points[i][0] < points[leftPointIndex][0])
            {
                leftPointIndex = i;
            }
        }

        int idx = leftPointIndex;
        do
        {
            int next = (idx + 1) % points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                if (Orientation(points[idx], points[i], points[next]) == 2)
                {
                    next = i;
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (Orientation(points[idx], points[i], points[next]) == 0)
                {
                    res.Add(points[i]);
                }
            }

            idx = next;
        } while (idx != leftPointIndex);

        return res;
    }

    private int Orientation(int[] p, int[] q, int[] r)
    {
        int val = (q[1] - p[1]) * (r[0] - q[0]) - (q[0] - p[0]) * (r[1] - q[1]);
        if (val == 0)
        {
            return 0;
        }

        return (val > 0) ? 1 : 2;
    }
    
    

    private bool OnSegment(int[] p, int[] q, int[] r) =>
        r[0] >= Math.Min(p[0], q[0]) &&
        r[0] <= Math.Max(p[0], q[0]) &&
        r[1] >= Math.Min(p[1], q[1]) &&
        r[1] <= Math.Max(p[1], q[1]);
}