// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1779
{
    public int NearestValidPoint(int x, int y, int[][] points)
    {

        int minDistance = int.MaxValue;
        int minIndex = -1;
        for (int i = 0; i < points.Length; i++)
        {
            if (IsValid(points[i], x, y))
            {
                var distance = Distance(points[i], x, y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = i;
                }
            }
        }

        return minIndex;
    }

    private bool IsValid(int[] point, int x, int y) =>
        x == point[0] || y == point[1];

    private int Distance(int[] point, int x, int y) =>
        Math.Abs(point[0] - x) + Math.Abs(point[1] - y);
}