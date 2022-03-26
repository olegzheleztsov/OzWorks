// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Microsoft.VisualBasic;
using System;
using System.Linq;

namespace Oz.LeetCode;

public class Leet42
{
    public int Trap(int[] height)
    {
        var maxLeft = new int[height.Length];
        var maxRight = new int[height.Length];

        var curMax = 0;
        maxLeft[0] = curMax;
        curMax = height[0];

        for (var i = 1; i < height.Length; i++)
        {
            maxLeft[i] = curMax;
            curMax = Math.Max(curMax, height[i]);
        }

        curMax = 0;
        maxRight[height.Length - 1] = curMax;
        curMax = height[height.Length - 1];
        for (var i = height.Length - 2; i >= 0; i--)
        {
            maxRight[i] = curMax;
            curMax = Math.Max(curMax, height[i]);
        }

        Console.WriteLine(Strings.Join(maxLeft.Select(n => n.ToString()).ToArray(), ", "));
        Console.WriteLine(Strings.Join(maxRight.Select(n => n.ToString()).ToArray(), ", "));
        var result = 0;

        for (var i = 0; i < height.Length; i++)
        {
            var val = Math.Min(maxLeft[i], maxRight[i]) - height[i];
            if (val > 0)
            {
                result += val;
            }
        }

        return result;
    }

    public static void Test()
    {
        var obj = new Leet42();
        obj.Trap(new[] {0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1});
    }
}