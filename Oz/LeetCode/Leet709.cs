// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet709
{
    public string ToLowerCase(string s)
    {
        var sb = new StringBuilder();
        foreach (var c in s)
        {
            if (c >= 'A' && c <= 'Z')
            {
                sb.Append(char.ToLower(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}