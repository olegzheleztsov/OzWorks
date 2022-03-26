// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet191
{
    public int HammingWeight(uint n)
    {
        var count = 0;

        for (var i = 0; i < 32; i++)
        {
            var x = n >> i;

            if ((x & 1) != 0)
            {
                count++;
            }
        }

        return count;
    }
}