// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Text;

namespace Oz.LeetCode;

public class Leet1309
{
    public string FreqAlphabets(string s)
    {

        Dictionary<string, char> map = new Dictionary<string, char>();
        char c = 'a';

        for (int i = 1; i <= 9; i++)
        {
            map.Add(i.ToString(), c);
            c++;
        }

        c = 'j';
        for (int i = 10; i <= 26; i++)
        {
            map.Add($"{i}#", c);
            c++;
        }

        StringBuilder acc = new StringBuilder();
        StringBuilder res = new StringBuilder();
        foreach (char current in s)
        {
            if (current == '#')
            {
                res.Append(map[acc.ToString() + "#"]);
                acc.Clear();
            }
            else if(acc.Length == 2)
            {
                char exitChar = acc[0];
                acc.Remove(0, 1);
                acc.Append(current);
                res.Append(map[exitChar.ToString()]);
            }
            else
            {
                acc.Append(current);
            }
        }

        foreach (char cc in acc.ToString())
        {
            res.Append(map[cc.ToString()]);
        }
        
        return res.ToString();
    }
}