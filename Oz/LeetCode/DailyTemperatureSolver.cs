#region

using Oz.Algorithms;
using System;
using System.Collections.Generic;

#endregion

namespace Oz.LeetCode;

public class DailyTemperatureSolver
{
    private static IEnumerable<int> DailyTemperatures(int[] T)
    {
        var answers = new int[T.Length];
        var next = new int[101];
        Array.Fill(next, int.MaxValue);
        for (var i = T.Length - 1; i >= 0; i--)
        {
            var warmerIndex = int.MaxValue;
            for (var t = T[i] + 1; t <= 100; t++)
            {
                if (next[t] < warmerIndex)
                {
                    warmerIndex = next[t];
                }
            }

            if (warmerIndex < int.MaxValue)
            {
                answers[i] = warmerIndex - i;
            }

            next[T[i]] = i;
        }

        return answers;
    }

    public static void Test()
    {
        var result = DailyTemperatures(new[] {73, 74, 75, 71, 69, 72, 76, 73});
        Console.WriteLine(result.GetStringRepresentation());
    }
}