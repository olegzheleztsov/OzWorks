// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _739
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        var stack = new Stack<TempIndex>();
        var result = new int[temperatures.Length];

        for (var i = 0; i < temperatures.Length; i++)
        {
            if (stack.Count == 0 || stack.Peek().Temperature >= temperatures[i])
            {
                stack.Push(new TempIndex
                {
                    Temperature = temperatures[i],
                    Index = i
                });
            }
            else
            {
                while (stack.Count > 0 && stack.Peek().Temperature < temperatures[i])
                {
                    var t = stack.Pop();
                    result[t.Index] = i - t.Index;
                }
                stack.Push(new TempIndex()
                {
                    Index = i,
                    Temperature = temperatures[i]
                });
            }
        }

        while (stack.Count > 0)
        {
            var t = stack.Pop();
            result[t.Index] = 0;
        }

        return result;
    }

    public struct TempIndex
    {
        public int Temperature { get; init; }
        public int Index { get; init; }
    }
}