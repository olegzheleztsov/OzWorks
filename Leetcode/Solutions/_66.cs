// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _66
{
    public int[] PlusOne(int[] digits)
    {
        var memo = 0;
        for (var i = digits.Length - 1; i >= 0; i--)
        {
            var val = digits[i] + (i == digits.Length - 1 ? 1 : 0) + memo;
            digits[i] = val % 10;
            memo = val / 10;
        }

        if (memo > 0)
        {
            var result = new int[digits.Length + 1];
            result[0] = memo;
            for (var i = 1; i < result.Length; i++)
            {
                result[i] = digits[i - 1];
            }

            digits = result;
        }

        return digits;
    }
}