// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

public class Leet785
{
    public bool IsBipartite(int[][] graph)
    {
        var nodeCounts = graph.Length;

        var codes = new bool?[nodeCounts];

        for (var i = 0; i < nodeCounts; i++)
        {
            if (codes[i] == null && !Dfs(i, graph, false, codes))
            {
                return false;
            }
        }

        return true;
    }

    private bool Dfs(int node, int[][] graph, bool currentCode, bool?[] codes)
    {
        if (codes[node] != null)
        {
            return codes[node] == currentCode;
        }

        codes[node] = currentCode;

        for (var i = 0; i < graph[node].Length; i++)
        {
            if (!Dfs(graph[node][i], graph, !currentCode, codes))
            {
                return false;
            }
        }

        return true;
    }
}