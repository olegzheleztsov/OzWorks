// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

public class Leet1615
{
    public int MaximalNetworkRank(int n, int[][] roads)
    {
        var res = 0;
        var inDegree = new Dictionary<int, int>();
        var graph = new Dictionary<int, HashSet<int>>();

        foreach (var road in roads)
        {
            if (!inDegree.ContainsKey(road[0]))
            {
                inDegree.Add(road[0], 0);
            }

            if (!inDegree.ContainsKey(road[1]))
            {
                inDegree.Add(road[1], 0);
            }

            inDegree[road[0]]++;
            inDegree[road[1]]++;

            if (!graph.ContainsKey(road[0]))
            {
                graph.Add(road[0], new HashSet<int>());
            }

            if (!graph.ContainsKey(road[1]))
            {
                graph.Add(road[1], new HashSet<int>());
            }

            graph[road[0]].Add(road[1]);
            graph[road[1]].Add(road[0]);
        }

        var ranks = inDegree.OrderByDescending(x => x.Value).Select(x => new[] {x.Key, x.Value}).ToArray();

        for (var i = 0; i < ranks.Length - 1; i++)
        {
            for (var j = i + 1; j < ranks.Length; j++)
            {
                res = Math.Max(res,
                    ranks[i][1] + (graph[ranks[i][0]].Contains(ranks[j][0]) ? ranks[j][1] - 1 : ranks[j][1]));
            }
        }

        return res;
    }
}