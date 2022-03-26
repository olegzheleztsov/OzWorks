// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1356
{
    public int[] SortByBits(int[] arr)
    {
        Array.Sort(arr, CompareValues);
        return arr;
    }

    private int CompareValues(int a, int b)
    {
        var aOnes = NumberOfOnes(a);
        var bOnes = NumberOfOnes(b);
        if (aOnes < bOnes)
        {
            return -1;
        }

        if (aOnes > bOnes)
        {
            return 1;
        }

        if (a < b)
        {
            return -1;
        }

        if (a > b)
        {
            return 1;
        }

        return 0;
    }

    public int NumberOfOnes(int value)
    {
        var count = 0;
        while (value > 0)
        {
            if ((value & 1) != 0)
            {
                count++;
            }

            value >>= 1;
        }

        return count;
    }
}