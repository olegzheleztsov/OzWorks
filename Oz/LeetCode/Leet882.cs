// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet882
{
    //something wrong
    public int ReachableNodes(int[][] edges, int maxMoves, int n)
    {
        var adjacents = new Dictionary<int, List<(int x, int w)>>();

        foreach (var nodeArr in edges)
        {
            var x = nodeArr[0];
            var y = nodeArr[1];
            var c = nodeArr[2];
            if (adjacents.ContainsKey(x))
            {
                adjacents[x].Add((y, c + 1));
            }
            else
            {
                adjacents.Add(x, new List<(int x, int w)> {(y, c + 1)});
            }

            if (adjacents.ContainsKey(y))
            {
                adjacents[y].Add((x, c + 1));
            }
            else
            {
                adjacents.Add(y, new List<(int x, int w)> {(x, c + 1)});
            }
        }

        var heap = new List<(int dist, int node)> {(0, 0)};
        var dist = new int[edges.Length];
        for (var i = 0; i < dist.Length; i++)
        {
            dist[i] = int.MaxValue;
        }

        while (heap.Count > 0)
        {
            var (d, node) = heap[0];
            heap.RemoveAt(0);
            foreach (var (neigh, w) in adjacents[node])
            {
                var temp = d + w;
                if (temp >= dist[neigh])
                {
                    continue;
                }

                dist[neigh] = temp;
                heap.Add((dist[neigh], neigh));
            }
        }

        var ans = dist.Count(t => t <= maxMoves);

        foreach (var edgeArr in edges)
        {
            var x = edgeArr[0];
            var y = edgeArr[1];
            var w = edgeArr[2];

            var (wx, wy) = (maxMoves - dist[x], maxMoves - dist[y]);
            if (wx >= 0 && wy >= 0)
            {
                ans += wx + wy - Math.Max(wx + wy - w, 0);
            }
            else
            {
                ans += Math.Max(wx, 0) + Math.Max(wy, 0);
            }
        }

        return ans;
    }
}