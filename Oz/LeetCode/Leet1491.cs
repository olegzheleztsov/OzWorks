// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1491
{
    public double Average(int[] salary)
    {
        Array.Sort(salary);
        int total = 0;
        for (int i = 1; i < salary.Length - 1; i++)
        {
            total += salary[i];
        }

        return total / (double)(salary.Length - 2);
    }
}