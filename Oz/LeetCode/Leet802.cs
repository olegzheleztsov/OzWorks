// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet802
{
    public IList<int> EventualSafeNodes(int[][] graph)
    {
        HashSet<int> cyclicNodes = new HashSet<int>();
        var graphNodes = Enumerable.Range(0, graph.Length).ToList();
        var states = graphNodes.ToDictionary(x => x, x => State.Unseen);

        bool Dfs(int node)
        {
            if (states[node] == State.Visited)
            {
                cyclicNodes.Add(node);
            } else if (states[node] == State.Unseen)
            {
                states[node] = State.Visited;
                foreach (var n in graph[node])
                {
                    if (!Dfs(n))
                    {
                        cyclicNodes.Add(node);
                    }
                }

                states[node] = State.Completed;
            }

            return !cyclicNodes.Contains(node);
        }
        graphNodes.ForEach((n) => Dfs(n));
        return graphNodes.Except(cyclicNodes).ToList();
    }
    
    enum State { Unseen, Visited, Completed }
}