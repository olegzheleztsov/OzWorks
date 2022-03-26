// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet518
{
    public int Change(int amount, int[] coins)
    {
        var dp = new int[amount + 1, coins.Length + 1];

        for (var i = 0; i <= coins.Length; i++)
        {
            dp[0, i] = 1;
        }

        for (var amnt = 1; amnt <= amount; amnt++)
        {
            for (var j = 1; j <= coins.Length; j++)
            {
                var currAmount = amnt;
                dp[amnt, j] = dp[amnt, j - 1];

                while (true)
                {
                    currAmount -= coins[j - 1];
                    if (currAmount < 0)
                    {
                        break;
                    }

                    dp[amnt, j] += dp[currAmount, j - 1];
                }
            }
        }

        return dp[amount, coins.Length];
    }
}