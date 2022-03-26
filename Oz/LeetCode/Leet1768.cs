// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet1768
{
    public string MergeAlternately(string word1, string word2)
    {

        StringBuilder stringBuilder = new StringBuilder();
        int first = 0, second = 0;
        bool isFirst = true;

        while (first < word1.Length && second < word2.Length)
        {
            if (isFirst)
            {
                stringBuilder.Append(word1[first]);
                first++;
                isFirst = false;
            }
            else
            {
                stringBuilder.Append(word2[second]);
                second++;
                isFirst = true;
            }
        }

        while (first < word1.Length)
        {
            stringBuilder.Append(word1[first]);
            first++;
        }

        while (second < word2.Length)
        {
            stringBuilder.Append(word2[second]);
            second++;
        }

        return stringBuilder.ToString();
    }
}