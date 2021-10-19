// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet_1414
{
    public int FindMinFibonacciNumbers(int k)
    {
        var fibs = new List<int> {1, 1};
        var nextFib = fibs[^2] + fibs[^1];
        while (nextFib <= k)
        {
            fibs.Add(nextFib);
            nextFib = fibs[^2] + fibs[^1];
        }

        fibs.Add(nextFib);

        var count = 0;
        var rem = k;
        while (rem > 0)
        {
            for (var i = 0; i < fibs.Count - 1; i++)
            {
                if (fibs[i] > rem || fibs[i + 1] <= rem)
                {
                    continue;
                }

                rem -= fibs[i];
                count++;
                break;
            }
        }

        return count;
    }
}