// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _989
{
    public IList<int> AddToArrayForm(int[] num, int k)
    {
        var ka = ToArrayForm(k);

        var first = num.Length >= ka.Length ? num : ka;
        var second = num.Length < ka.Length ? num : ka;

        var i = first.Length - 1;
        var j = second.Length - 1;

        var memo = 0;
        var result = new List<int>();

        for (; i >= 0; i--, j--)
        {
            if (j >= 0)
            {
                var val = (first[i] + second[j] + memo) % 10;
                memo = (first[i] + second[j] + memo) / 10;
                result.Insert(0, val);
            }
            else
            {
                var val = (first[i] + memo) % 10;
                memo = (first[i] + memo) / 10;
                result.Insert(0, val);
            }
        }

        if (memo > 0)
        {
            result.Insert(0, memo);
        }

        return result.ToArray();
    }

    private int[] ToArrayForm(int k)
    {
        var result = new List<int>();
        while (k > 0)
        {
            var mod = k % 10;
            result.Insert(0, mod);
            k /= 10;
        }

        return result.ToArray();
    }
}