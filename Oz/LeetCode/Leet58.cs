// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet58
{
    public int LengthOfLastWord(string s)
    {

        StringBuilder lastWordBuilder = new StringBuilder();

        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (char.IsWhiteSpace(s[i]))
            {
                if (lastWordBuilder.Length > 0)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                lastWordBuilder.Insert(0, s[i]);
            }
        }

        return lastWordBuilder.Length;
    }
}