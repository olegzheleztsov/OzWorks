// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public class Leet997
{
    public int FindJudge(int n, int[][] trust)
    {
        if (trust.Length == 0 && n == 1)
        {
            return 1;
        }

        var input = new Dictionary<int, int>();
        var output = new Dictionary<int, int>();
        foreach (var tr in trust)
        {
            if (output.ContainsKey(tr[0]))
            {
                output[tr[0]]++;
            }
            else
            {
                output.Add(tr[0], 1);
            }

            if (input.ContainsKey(tr[1]))
            {
                input[tr[1]]++;
            }
            else
            {
                input.Add(tr[1], 1);
            }
        }

        foreach (var (num, cnt) in input)
        {
            if (cnt == n - 1 && !output.ContainsKey(num))
            {
                return num;
            }
        }

        return -1;
    }
}