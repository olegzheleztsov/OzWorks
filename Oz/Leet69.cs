// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet69
{
    public int MySqrt(int x)
    {
        if (x == 1)
        {
            return 1;
        }

        var minValue = 1L;
        long maxValue = x;

        while (minValue < maxValue)
        {
            var mid = minValue + ((maxValue - minValue) / 2);
            var target = mid * mid;
            if (target == x)
            {
                return (int)mid;
            }

            if (target > x)
            {
                maxValue = mid - 1;
            }
            else
            {
                minValue = mid + 1;
            }
        }

        if (minValue * minValue <= x)
        {
            return (int)minValue;
        }

        return (int)minValue - 1;
    }
}