// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet901
{
    public class StockSpanner
    {
        private readonly List<int> _stocks = new();

        public int Next(int price)
        {
            _stocks.Add(price);
            var ind = _stocks.Count - 1;
            var count = 0;
            while (ind >= 0)
            {
                if (_stocks[ind] <= price)
                {
                    count++;
                }
                else
                {
                    break;
                }

                ind--;
            }

            return count;
        }
    }
}