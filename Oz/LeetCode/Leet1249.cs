// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.LeetCode;

public class Leet1249
{
    public string MinRemoveToMakeValid(string s)
    {
        var sb = new StringBuilder();
        var stack = new Stack<int>();
        var i = 0;
        foreach (var c in s)
        {
            switch (c)
            {
                case '(':
                {
                    stack.Push(i++);
                    sb.Append(c);
                }
                    break;
                case ')':
                {
                    if (stack.Any())
                    {
                        sb.Append(c);
                        stack.Pop();
                        i++;
                    }
                }
                    break;
                default:
                {
                    sb.Append(c);
                }
                    break;
            }
        }

        s = sb.ToString();
        while (stack.Count > 0)
        {
            s = s.Remove(stack.Pop(), 1);
        }

        return s;
    }
}