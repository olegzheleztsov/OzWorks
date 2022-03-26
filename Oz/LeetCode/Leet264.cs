// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet264
{
    public int NthUglyNumber(int n)
    {
        var uglyNumbers = new Dictionary<int, int>
        {
            [1] = 1,
            [2] = 2,
            [3] = 3,
            [4] = 4,
            [5] = 5
        };

        if (uglyNumbers.ContainsKey(n))
        {
            return uglyNumbers[n];
        }

        var uglyHash = new HashSet<int>
        {
            1,
            2,
            3,
            4,
            5
        };

        var maxUglyNumber = 5;

        var i = 6;
        while (i <= n)
        {
            var nextUgly = FindNextUglyNumber(uglyHash, maxUglyNumber);
            uglyHash.Add(nextUgly);
            maxUglyNumber = nextUgly;
            i++;
        }

        return maxUglyNumber;

        int FindNextUglyNumber(HashSet<int> cache, int maxPrev)
        {
            var num = maxPrev + 1;
            while (true)
            {
                if (num % 2 == 0)
                {
                    if (cache.Contains(num / 2))
                    {
                        return num;
                    }
                }

                if (num % 3 == 0)
                {
                    if (cache.Contains(num / 3))
                    {
                        return num;
                    }
                }

                if (num % 5 == 0)
                {
                    if (cache.Contains(num / 5))
                    {
                        return num;
                    }
                }

                num++;
            }
        }
    }

    public int EffectiveSolution(int n)
    {
        var arr = new int[n];
        arr[0] = 1;
        int p2 = 0, p3 = 0, p5 = 0;

        for (var i = 1; i < n; i++)
        {
            var num1 = 2 * arr[p2];
            var num2 = 3 * arr[p3];
            var num3 = 5 * arr[p5];

            arr[i] = Math.Min(num1, Math.Min(num2, num3));

            if (num1 == arr[i])
            {
                p2++;
            }

            if (num2 == arr[i])
            {
                p3++;
            }

            if (num3 == arr[i])
            {
                p5++;
            }
        }

        return arr[n - 1];
    }
}