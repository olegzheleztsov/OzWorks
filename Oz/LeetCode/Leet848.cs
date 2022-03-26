// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet848
{
    public string ShiftingLetters(string s, int[] shifts)
    {
        var sb = new StringBuilder(s);
        for (var i = 0; i < s.Length; i++)
        {
            for (var k = 0; k <= i; k++)
            {
                sb[k] = Shift(sb[k], shifts[i]);
            }
        }

        return sb.ToString();
    }

    private char Shift(char c, int count)
    {
        count %= 26;
        var number = (c - 'a' + count) % 26;
        return (char)('a' + number);
    }
}