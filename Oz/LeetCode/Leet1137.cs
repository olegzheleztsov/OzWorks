// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1137
{
    public int Tribonacci(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        if (n == 2)
        {
            return 1;
        }

        var trib = new int[n + 1];
        trib[0] = 0;
        trib[1] = 1;
        trib[2] = 1;

        for (var i = 3; i <= n; i++)
        {
            trib[i] = trib[i - 1] + trib[i - 2] + trib[i - 3];
        }

        return trib[n];
    }
}