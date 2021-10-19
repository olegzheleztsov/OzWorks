using System;
using System.Collections.Generic;

namespace Oz.LeetCode.Recursion;

public class LargestRectangleAreaSolver
{
    public int LargestRectangleArea(int[] heights)
    {
        var max = 0;
        var stack = new Stack<int>();

        for (var i = 0; i <= heights.Length; i++)
        {
            var height = i < heights.Length ? heights[i] : 0;
            while (stack.Count > 0 && heights[stack.Peek()] > height)
            {
                var currHeight = heights[stack.Pop()];
                var prevIndex = stack.Count == 0 ? -1 : stack.Peek();
                max = Math.Max(max, currHeight * (i - 1 - prevIndex));
            }

            stack.Push(i);
        }

        return max;
    }
}