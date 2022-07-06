// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Leetcode.Solutions;

public class _556
{
    public int NextGreaterElement(int n)
    {
        var digits = n.ToString().ToCharArray();
        var i = digits.Length - 1;

        while (i > 0 && digits[i - 1] >= digits[i])
        {
            i--;
        }

        if (i == 0)
        {
            return -1;
        }

        var min = digits.Skip(i).Where(d => d > digits[i - 1]).Min();
        var j = Array.IndexOf(digits, min, i);
        (digits[i - 1], digits[j]) = (digits[j], digits[i - 1]);
        Array.Sort(digits, i, digits.Length-i);
        var number = long.Parse(new string(digits));
        return number > int.MaxValue ? -1 : (int)number;
    }


}