// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet122
{
    public static int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length == 0)
        {
            return 0;
        }

        var result = 0;
        var minPrice = prices[0];

        for (var i = 1; i < prices.Length; i++)
        {
            if (prices[i - 1] > prices[i] && minPrice < prices[i - 1])
            {
                result += prices[i - 1] - minPrice;
                minPrice = prices[i];
            }
            else
            {
                minPrice = Math.Min(minPrice, prices[i]);
            }
        }

        result += prices[^1] - minPrice;
        return result;
    }
}