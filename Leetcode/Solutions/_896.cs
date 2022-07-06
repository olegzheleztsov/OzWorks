// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _896
{
    public bool IsMonotonic(int[] nums)
    {
        if (nums.Length <= 1)
        {
            return true;
        }

        var prevSign = 0;

        for (var i = 0; i < nums.Length - 1; i++)
        {
            var currectSign = 0;
            var diff = nums[i + 1] - nums[i];
            if (diff > 0)
            {
                currectSign = 1;
            }
            else if (diff < 0)
            {
                currectSign = -1;
            }

            if (prevSign != 0 && currectSign != 0 && prevSign != currectSign)
            {
                return false;
            }

            if (currectSign != 0)
            {
                prevSign = currectSign;
            }
        }

        return true;
    }
}