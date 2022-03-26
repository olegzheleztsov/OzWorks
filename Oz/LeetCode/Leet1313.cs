// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1313
{
    public int[] DecompressRLElist(int[] nums)
    {
        var size = 0;
        for (var i = 0; i < nums.Length; i += 2)
        {
            size += nums[i];
        }

        var result = new int[size];

        var index = 0;
        for (var i = 0; i < nums.Length - 1; i += 2)
        {
            var freq = nums[i];
            var val = nums[i + 1];
            var k = 0;
            while (k < freq)
            {
                result[index++] = val;
                k++;
            }
        }

        return result;
    }
}