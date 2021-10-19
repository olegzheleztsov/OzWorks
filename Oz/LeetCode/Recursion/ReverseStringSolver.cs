#region

using System;

#endregion

namespace Oz.LeetCode.Recursion;

public class ReverseStringSolver
{
    private static void ReverseString(char[] s)
    {
        if (s == null || s.Length <= 1)
        {
            return;
        }

        if (s.Length == 2)
        {
            (s[0], s[1]) = (s[1], s[0]);
            return;
        }

        var forwardIndex = 0;
        var backwardIndex = s.Length - 1;
        ReverseStringRecursion(s, forwardIndex, backwardIndex);
    }

    private static void ReverseStringRecursion(char[] s, int forwardIndex, int backwardIndex)
    {
        if (forwardIndex >= backwardIndex)
        {
            return;
        }

        ReverseStringRecursion(s, forwardIndex + 1, backwardIndex - 1);
        (s[forwardIndex], s[backwardIndex]) = (s[backwardIndex], s[forwardIndex]);
    }

    public static void Test1()
    {
        var str = new[] {'h', 'e', 'l', 'l', 'o'};
        ReverseString(str);
        Console.WriteLine(string.Join(' ', str));
    }
}