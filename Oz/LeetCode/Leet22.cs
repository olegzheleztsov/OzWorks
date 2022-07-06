// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.LeetCode;

public class Leet22
{
    public IList<string> GenerateParenthesis(int n)
    {
        StringBuilder current = new();
        List<string> results = new();
        Backtrack(current, n, n, results);
        return results;
    }

    private void Backtrack(StringBuilder current, int openedCount, int closedCount, List<string> results)
    {
        if (closedCount == 0)
        {
            results.Add(current.ToString());
            return;
        }

        if (openedCount == closedCount)
        {
            current.Append('(');
            Backtrack(current, openedCount-1, closedCount, results);
            current.Remove(current.Length - 1, 1);
        } else if (openedCount > 0)
        {
            current.Append('(');
            Backtrack(current, openedCount-1, closedCount, results);
            current.Remove(current.Length - 1, 1);
        } else if (closedCount > 0)
        {
            current.Append(')');
            Backtrack(current, openedCount-1, closedCount, results);
            current.Remove(current.Length - 1, 1);
        }
    }
}