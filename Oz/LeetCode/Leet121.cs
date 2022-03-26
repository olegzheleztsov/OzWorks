// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet121
{
    public int MaxProfit(int[] prices)
    {
        var buyPrices = new int[prices.Length];
        buyPrices[0] = prices[0];

        for (var i = 1; i < prices.Length; i++)
        {
            var buyPrice = Math.Min(buyPrices[i - 1], prices[i]);
            buyPrices[i] = buyPrice;
        }

        var maxDiff = 0;
        for (var i = 1; i < prices.Length; i++)
        {
            var diff = prices[i] - buyPrices[i - 1];
            maxDiff = Math.Max(maxDiff, diff);
        }

        return maxDiff;
    }
}