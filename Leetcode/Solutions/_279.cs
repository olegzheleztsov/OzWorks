// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _279
{
    public int NumSquares(int n)
    {
        if (n <= 1)
        {
            return 1;
        }

        var dp = new int[n + 1];
        dp[0] = 1;
        dp[1] = 1;


        for (var i = 2; i <= n; i++)
        {
            var sqrti = (int)Math.Sqrt(i);
            if (sqrti * sqrti == i)
            {
                dp[i] = 1;
            }
            else
            {
                var min = int.MaxValue;
                for (var k = 1; k * k <= i; k++)
                {
                    min = Math.Min(min, dp[i - (k * k)]);
                }

                dp[i] = min + 1;
            }
        }

        return dp[n];
    }
}