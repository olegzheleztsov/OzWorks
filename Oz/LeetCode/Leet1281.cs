// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1281
{
    public int SubtractProductAndSum(int n)
    {
        int prod = 1;
        int sum = 0;

        while (n != 0)
        {
            int digit = n % 10;
            prod *= digit;
            sum += digit;
            n -= digit;
            n /= 10;
        }

        return prod - sum;
    }
}