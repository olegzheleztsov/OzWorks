// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet190
{
    public uint reverseBits(uint n)
    {
        uint result = 0;
        var count = 0;
        while (count < 32)
        {
            var current = n;
            result <<= 1;
            result = result | (current & 1);
            n >>= 1;
            count++;
        }

        return result;
    }
}