// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _503
{
    public int[] NextGreaterElements(int[] nums)
    {
        var result = new int[nums.Length];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = int.MinValue;
        }

        var stack = new Stack<Pair>();
        var nextIndex = 0;
        var counter = 0;
        var maxElement = nums.Max();
        var maxCount = nums.Count(n => n == maxElement);
        var found = 0;

        while (found < nums.Length - maxCount)
        {
            var testValue = nums[nextIndex];

            if (stack.Count == 0 || stack.Peek().Value >= testValue)
            {
                stack.Push(new Pair(testValue, nextIndex));
            }
            else
            {
                while (stack.Count > 0 && stack.Peek().Value < testValue)
                {
                    var stackTopVal = stack.Pop();
                    if (result[stackTopVal.Index] == int.MinValue)
                    {
                        result[stackTopVal.Index] = testValue;
                        found++;
                    }
                }

                stack.Push(new Pair(testValue, nextIndex));
            }

            nextIndex++;
            if (nextIndex >= nums.Length)
            {
                nextIndex = 0;
            }

            counter++;
        }

        for (var i = 0; i < result.Length; i++)
        {
            if (result[i] == int.MinValue)
            {
                result[i] = -1;
            }
        }

        return result;
    }

    public record Pair(int Value, int Index);
}