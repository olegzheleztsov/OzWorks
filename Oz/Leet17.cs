// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Text;

public sealed class Leet17
{
    public IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits))
        {
            return new List<string>();
        }

        var mapping = new Dictionary<char, string>
        {
            ['2'] = "abc",
            ['3'] = "def",
            ['4'] = "ghi",
            ['5'] = "jkl",
            ['6'] = "mno",
            ['7'] = "pqrs",
            ['8'] = "tuv",
            ['9'] = "wxyz"
        };
        List<string> results = new();
        StringBuilder sb = new();
        Backtrack(mapping, results, sb, 0, digits);
        return results;
    }

    private void Backtrack(Dictionary<char, string> mapping, List<string> results, StringBuilder current, int index,
        string digits)
    {
        if (current.Length == digits.Length)
        {
            results.Add(current.ToString());
            return;
        }

        var nextStr = mapping[digits[index]];
        foreach (var c in nextStr)
        {
            current.Append(c);
            Backtrack(mapping, results, current, index + 1, digits);
            current.Remove(current.Length - 1, 1);
        }
    }
}