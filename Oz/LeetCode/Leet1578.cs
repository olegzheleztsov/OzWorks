// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1578
{
    public int MinCost(string colors, int[] neededTime)
    {

        int groupSum = 0;
        int groupMax = 0;
        int answer = 0;

        for (int i = 0; i < colors.Length; i++)
        {
            if (i > 0 && colors[i] != colors[i - 1])
            {
                answer += (groupSum - groupMax);
                groupSum = 0;
                groupMax = 0;
            }
            groupSum += neededTime[i];
            groupMax = Math.Max(groupMax, neededTime[i]);
        }

        answer += groupSum - groupMax;
        return answer;
    }
    
}