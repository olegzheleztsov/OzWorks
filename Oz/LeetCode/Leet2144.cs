// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Linq;

namespace Oz.LeetCode;

public class Leet2144
{
    public int MinimumCost(int[] cost)
    {
        cost = cost.OrderByDescending(v => v).ToArray();

        int counter = 0;
        int sum = 0;
        for (int i = 0; i < cost.Length; i++)
        {
            switch (counter % 3)
            {
                case 0:
                case 1:
                {
                    sum += cost[i];
                }
                    break;
            }

            counter++;
        }

        return sum;
    }
}