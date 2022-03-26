// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet1819
{
    public int CountDifferentSubsequenceGCDs(int[] nums)
    {
        var maxValue = nums.Max();
        var sequenceSet = new HashSet<int>(nums);

        var answer = 0;
        for (var x = 1; x <= maxValue; x++)
        {
            var currentGcd = 0;
            for (var test = x; test <= maxValue; test += x)
            {
                if (sequenceSet.Contains(test))
                {
                    currentGcd = Gcd(currentGcd, test);
                    if (currentGcd == x)
                    {
                        answer++;
                        break;
                    }
                }
            }
        }

        return answer;
    }

    private int Gcd(int a, int b)
    {
        while (b > 0)
        {
            (a, b) = (b, a % b);
        }

        return a;
    }
}