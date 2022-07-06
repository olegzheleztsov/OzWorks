// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet844
{
    public bool BackspaceCompare(string s, string t)
    {
        var s1 = GetBackspacedString(s);
        var t1 = GetBackspacedString(t);
        return s1 == t1;
    }

    private string GetBackspacedString(string s)
    {
        var stack = new Stack<char>();
        foreach (var c in s)
        {
            if (c == '#')
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                stack.Push(c);
            }
        }

        var arr = stack.ToArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}