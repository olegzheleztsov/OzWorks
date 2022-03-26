using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.ProblemSolving;

public class Problems
{
    //Hacker rank task about apples and oranges
    private static void countApplesAndOranges(int s, int t, int a, int b, int[] apples, int[] oranges)
    {
        var appleCount = 0;
        var orangeCount = 0;
        if (apples != null && apples.Length > 0)
        {
            appleCount = apples.Select(element => a + element).Count(element => s <= element && element <= t);
        }

        if (oranges != null && oranges.Length > 0)
        {
            orangeCount = oranges.Select(element => b + element).Count(element => s <= element && element <= t);
        }

        Console.WriteLine(appleCount);
        Console.WriteLine(orangeCount);
    }

    private static string kangaroo(int x1, int v1, int x2, int v2)
    {
        var dx = x1 - x2;
        var dv = v2 - v1;
        if (dv == 0)
        {
            return dx == 0 ? "YES" : "NO";
        }

        if (dx % dv != 0)
        {
            return "NO";
        }

        var t = dx / dv;
        return t < 0 ? "NO" : "YES";
    }

    public static int getTotalX(List<int> a, List<int> b)
    {
        if (a == null || b == null || a.Count == 0 || b.Count == 0)
        {
            return 0;
        }

        var maxA = a.Max();
        var minB = b.Min();

        var count = 0;
        for (var i = maxA; i <= minB; i++)
        {
            if (a.All(element => i % element == 0) &&
                b.All(element => element % i == 0))
            {
                count++;
            }
        }

        return count;
    }
}