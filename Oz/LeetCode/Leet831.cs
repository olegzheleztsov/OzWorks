// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Linq;
using System.Text;

namespace Oz.LeetCode;

public class Leet831
{
    public string MaskPii(string s) =>
        IsEmail(s) ? MaskEmail(s) : MaskPhone(s);

    private bool IsEmail(string s) => s.Contains("@");

    private string MaskPhone(string s)
    {
        StringBuilder sb = new();
        foreach (var t in s.Where(t => char.IsDigit(t)))
        {
            sb.Append(t);
        }

        var numbers = sb.ToString();

        return numbers.Length switch
        {
            10 => $"***-***-{numbers.Substring(6)}",
            11 => $"+*-***-***-{numbers.Substring(7)}",
            12 => $"+**-***-***-{numbers.Substring(8)}",
            13 => $"+***-***-***-{numbers.Substring(9)}",
            _ => throw new InvalidOperationException()
        };
    }

    private string MaskEmail(string s)
    {
        var nameDomain = s.Split('@', StringSplitOptions.RemoveEmptyEntries);

        var name = MaskEmailSubstring(nameDomain[0]);
        var domain = MaskEmailDomain(nameDomain[1]);
        return name + '@' + domain;
    }

    private string MaskEmailDomain(string s)
    {
        StringBuilder sb = new();
        foreach (var t in s)
        {
            sb.Append(char.IsUpper(t) ? char.ToLower(t) : t);
        }

        return sb.ToString();
    }

    private string MaskEmailSubstring(string s)
    {
        StringBuilder sb = new();
        sb.Append(char.IsUpper(s[0]) ? char.ToLower(s[0]) : s[0]);

        sb.Append("*****");

        sb.Append(char.IsUpper(s[^1]) ? char.ToLower(s[^1]) : s[^1]);
        return sb.ToString();
    }
}