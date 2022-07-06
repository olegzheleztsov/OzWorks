// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1786
{
    public int CountRestrictedPaths(int n, int[][] edges)
    {
        var graph = new Dictionary<int, List<Tuple<int, int>>>();
        foreach (var edge in edges)
        {
            AddNodeToGraph(edge[0], edge[1], edge[2], graph);
            AddNodeToGraph(edge[1], edge[0], edge[2], graph);
        }

        var distanceToLastNode = new Dictionary<int, int>();
        var priorityQueue = new SortedSet<Tuple<int, int>>
        {
            Tuple.Create(0, n)
        };

        while (priorityQueue.Count > 0 && distanceToLastNode.Count < n)
        {
            var min = priorityQueue.Min;
            var minDist = min.Item1;
            var minNode = min.Item2;
            priorityQueue.Remove(min);

            if (distanceToLastNode.ContainsKey(minNode))
            {
                continue;
            }

            distanceToLastNode.Add(minNode, minDist);

            var nodeDists = graph[minNode];
            foreach (var nodeDist in nodeDists)
            {
                var node = nodeDist.Item1;
                var dist = nodeDist.Item2;
                if (!distanceToLastNode.ContainsKey(node))
                {
                    priorityQueue.Add(Tuple.Create(minDist + dist, node));
                }
            }
        }

        return DFS(1, n, new int?[n + 1], graph, distanceToLastNode);
    }

    private void AddNodeToGraph(int nodeFrom, int nodeTo, int dist, Dictionary<int, List<Tuple<int, int>>> graph)
    {
        if (!graph.ContainsKey(nodeFrom))
        {
            graph[nodeFrom] = new List<Tuple<int, int>>();
        }

        graph[nodeFrom].Add(Tuple.Create(nodeTo, dist));
    }

    private int DFS(int currNode, int target, int?[] dp, Dictionary<int, List<Tuple<int, int>>> graph, Dictionary<int, int> distanceToLastNode)
    {
        if (dp[currNode].HasValue)
        {
            return dp[currNode].Value;
        }

        if (currNode == target)
        {
            return 1;
        }

        var result = 0;
        var nodeDists = graph[currNode];
        foreach (var nodeDist in nodeDists)
        {
            var nextNode = nodeDist.Item1;
            if (distanceToLastNode[currNode] > distanceToLastNode[nextNode])
            {
                result = (result + DFS(nextNode, target, dp, graph, distanceToLastNode)) % (int)(1e9 + 7);
            }
        }

        dp[currNode] = result;
        return result;
    }
}