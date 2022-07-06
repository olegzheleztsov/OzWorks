// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _67
{
    public string AddBinary(string a, string b)
    {
        var first = a.Length >= b.Length ? a : b;
        var second = a.Length < b.Length ? a : b;

        int i = first.Length - 1, j = second.Length - 1;
        var result = new List<char>();
        var memo = 0;
        for (; i >= 0; i--, j--)
        {
            if (j >= 0)
            {
                var na = int.Parse(first[i].ToString());
                var nb = int.Parse(second[j].ToString());
                var val = (na + nb + memo) % 2;
                memo = (na + nb + memo) / 2;
                result.Insert(0, val != 0 ? '1' : '0');
            }
            else
            {
                var na = int.Parse(first[i].ToString());
                var val = (na + memo) % 2;
                memo = (na + memo) / 2;
                result.Insert(0, val != 0 ? '1' : '0');
            }
        }

        if (memo != 0)
        {
            result.Insert(0, '1');
        }

        return new string(result.ToArray());
    }
}