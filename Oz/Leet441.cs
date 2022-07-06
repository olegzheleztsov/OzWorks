// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet441
{
    public int ArrangeCoins(int n)
    {
        var minValue = 1;
        var maxValue = n;

        while (minValue < maxValue)
        {
            var mid = minValue + ((maxValue - minValue) / 2);
            var sum = mid * (long)(mid + 1) / 2L;
            if (sum == n)
            {
                return mid;
            }

            if (sum > n)
            {
                maxValue = mid - 1;
            }

            if (sum < n)
            {
                minValue = mid + 1;
            }
        }

        long finalSum = minValue * (minValue + 1) / 2;
        if (finalSum <= n)
        {
            return minValue;
        }

        return minValue - 1;
    }
}