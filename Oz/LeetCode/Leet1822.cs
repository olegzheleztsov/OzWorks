// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1822
{
    public int ArraySign(int[] nums)
    {
        int result = 1;
        foreach (var num in nums)
        {
            if (num == 0)
            {
                return 0;
            }

            result *= SignFunc(num);
        }

        return result;
    }

    private int SignFunc(int val)
    {
        if (val == 0)
        {
            return 0;
        }

        return val > 0 ? 1 : -1;
    }
}