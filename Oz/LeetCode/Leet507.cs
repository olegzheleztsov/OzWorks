// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet507
{
    public bool CheckPerfectNumber(int num)
    {
        var sum = 0;
        for (var i = 1; i <= num / 2; i++)
        {
            if (num % i == 0)
            {
                sum += i;
            }
        }

        return sum == num;
    }
}