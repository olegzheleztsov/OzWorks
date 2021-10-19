// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet_791
{
    /// <summary>
    ///     https://leetcode.com/problems/custom-sort-string/
    /// </summary>
    public string CustomSortString(string order, string s)
    {
        if (order.Length <= 1)
        {
            return s;
        }

        var orderArray = order.ToCharArray();
        var sArray = s.ToCharArray();
        var orderDict = new Dictionary<char, int>();
        for (var i = 0; i < orderArray.Length; i++)
        {
            orderDict[orderArray[i]] = i;
        }

        for (var i = 0; i < sArray.Length; i++)
        {
            for (var j = i; j > 0 && Key(sArray[j], orderDict) < Key(sArray[j - 1], orderDict); j--)
            {
                (sArray[j], sArray[j - 1]) = (sArray[j - 1], sArray[j]);
            }
        }

        return new string(sArray);
    }

    private int Key(char c, Dictionary<char, int> orderDictionary)
    {
        if (orderDictionary.TryGetValue(c, out var key))
        {
            return key;
        }

        return int.MaxValue;
    }
}