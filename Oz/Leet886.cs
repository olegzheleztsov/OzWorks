// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public class Leet886
{
    public bool PossibleBipartition(int n, int[][] dislikes)
    {
        var graph = new List<int>[n + 1];

        for (var i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var dislike in dislikes)
        {
            graph[dislike[0]].Add(dislike[1]);
            graph[dislike[1]].Add(dislike[0]);
        }

        var locations = new int[n + 1];
        for (var i = 1; i <= n; i++)
        {
            if (!Partition(graph, i, locations[i], locations))
            {
                return false;
            }
        }

        return true;
    }

    private bool Partition(List<int>[] graph, int i, int value, int[] locations)
    {
        if (value == 0)
        {
            value = 1;
        }

        if (locations[i] == value)
        {
            return true;
        }

        if (locations[i] == -value)
        {
            return false;
        }

        locations[i] = value;

        foreach (var j in graph[i])
        {
            if (!Partition(graph, j, -value, locations))
            {
                return false;
            }
        }

        return true;
    }
}