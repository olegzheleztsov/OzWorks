// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

public class Leet11
{
    public int MaxArea(int[] height)
    {
        if (height.Length == 2)
        {
            return Math.Min(height[0], height[1]) * 1;
        }

        var leftIndex = 0;
        var rightIndex = height.Length - 1;
        var maxArea = Math.Min(height[leftIndex], height[rightIndex]) * (rightIndex - leftIndex);
        while (leftIndex < rightIndex)
        {
            var suggestedArea = Math.Min(height[leftIndex], height[rightIndex]) * (rightIndex - leftIndex);
            maxArea = Math.Max(maxArea, suggestedArea);
            if (height[leftIndex] < height[rightIndex])
            {
                leftIndex++;
            }
            else
            {
                rightIndex--;
            }
        }

        return maxArea;
    }
}