// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet1946
{
    public string MaximumNumber(string num, int[] change)
    {
        var sb = new StringBuilder();
        var sequenceFound = false;
        var mutated = false;
        foreach (var source in num)
        {
            var sourceVal = IntVal(source);
            var candidate = change[sourceVal];

            if (!sequenceFound)
            {
                if (candidate > sourceVal)
                {
                    sb.Append(candidate.ToString());
                    mutated = true;
                }
                else if (candidate == sourceVal)
                {
                    sb.Append(candidate.ToString());
                }
                else if (candidate < sourceVal)
                {
                    if (!mutated)
                    {
                        sb.Append(sourceVal);
                    }
                    else
                    {
                        sb.Append(sourceVal);
                        sequenceFound = true;
                    }
                }
            }
            else
            {
                sb.Append(sourceVal);
            }
        }

        return sb.ToString();

        int IntVal(char c)
        {
            return int.Parse(c.ToString());
        }
    }
}