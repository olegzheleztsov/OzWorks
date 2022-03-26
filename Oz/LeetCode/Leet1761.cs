// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1761
{
    public static void Test()
    {
        int[][] edges = new int[6][];
        edges[0] = new int[] {1, 2};
        edges[1] = new int[] {1, 3};
        edges[2] = new int[] {3, 2};
        edges[3] = new int[] {4, 1};
        edges[4] = new int[] {5, 2};
        edges[5] = new int[] {3, 6};
        Leet1761 leet1761 = new Leet1761();
        leet1761.MinTrioDegree(6, edges);
    }
    public int MinTrioDegree(int n, int[][] edges)
    {
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        Dictionary<int, int> degree = new Dictionary<int, int>();
        
        foreach(int[] edge in edges)
        {
            int u = Math.Min(edge[0], edge[1]);
            int v = Math.Max(edge[0], edge[1]);

            if (graph.ContainsKey(u))
            {
                graph[u].Add(v);
            }
            else
            {
                graph.Add(u, new List<int>(){v});
            }

            if (degree.ContainsKey(u))
            {
                degree[u]++;
            }
            else
            {
                degree.Add(u, 1);
            }

            if (degree.ContainsKey(v))
            {
                degree[v]++;
            }
            else
            {
                degree.Add(v, 1);
            }
        }

        int ans = int.MaxValue;

        for (int i = 1; i <= n; i++)
        {
            if (graph.ContainsKey(i))
            {
                foreach (var n2 in graph[i])
                {
                    foreach (var n3 in graph[i])
                    {
                        if (!graph.ContainsKey(n2))
                        {
                            graph.Add(n2, new List<int>());
                        }
                        if (graph[n2].Contains(n3))
                        {
                            if (degree.ContainsKey(i) && degree.ContainsKey(n2) && degree.ContainsKey(n3))
                            {
                                ans = Math.Min(ans, degree[i] + degree[n2] + degree[n3] - 6);
                            }
                        }
                    }
                }
            }
        }

        if (ans == int.MaxValue)
        {
            return -1;
        }

        return ans;
    }
}