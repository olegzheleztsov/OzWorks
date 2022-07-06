// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet986
{
    public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
    {
        var res = new List<int[]>();
        var i = 0;
        var j = 0;

        while (i < firstList.Length && j < secondList.Length)
        {
            var first = firstList[i];
            var second = secondList[j];

            if (second[0] > first[1])
            {
                i++;
                continue;
            }

            if (second[1] < first[0])
            {
                j++;
                continue;
            }

            res.Add(new[] {Math.Max(first[0], second[0]), Math.Min(first[1], second[1])});
            if (first[1] < second[1])
            {
                i++;
            }
            else
            {
                j++;
            }
        }

        return res.ToArray();
    }
}