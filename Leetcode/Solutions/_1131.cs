// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1131
{
    public int MaxAbsValExpr(int[] arr1, int[] arr2)
    {
        var len = arr1.Length;

        var max = int.MinValue;
        for (var i = 0; i < len; i++)
        {
            for (var j = 0; j < len; j++)
            {
                var t = MyAbsSunMax3(arr1[i] - arr1[j], arr2[i] - arr2[j], i - j);
                max = Math.Max(t, max);
            }
        }

        return max;
    }

    private int MyAbsSumMax2(int a, int b) =>
        Math.Max(Math.Max(a + b, a - b), Math.Max(-a + b, -a - b));

    private int MyAbsSunMax3(int a, int b, int c)
    {
        var t = MyAbsSumMax2(a, b);
        return MyAbsSumMax2(t, c);
    }
}