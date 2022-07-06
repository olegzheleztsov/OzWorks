// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public class Leet1557
{
    public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
    {
        var input = new Dictionary<int, int>();
        foreach (var edge in edges)
        {
            if (!input.ContainsKey(edge[1]))
            {
                input.Add(edge[1], 1);
            }
            else
            {
                input[edge[1]] = 1;
            }
        }

        var result = new List<int>();
        for (var i = 0; i < n; i++)
        {
            if (!input.ContainsKey(i))
            {
                result.Add(i);
            }
        }

        return result;
    }
}