// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

public sealed class Leet797
{
    public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        var result = new List<IList<int>>();
        var path = new List<int> {0};
        var queue = new Queue<List<int>>();
        queue.Enqueue(path);

        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            var last = path.Last();
            if (last == graph.Length - 1)
            {
                result.Add(new List<int>(path));
            }
            else
            {
                for (var i = 0; i < graph[last].Length; i++)
                {
                    if (!path.Contains(graph[last][i]))
                    {
                        var newPath = new List<int>(path);
                        newPath.Add(graph[last][i]);
                        queue.Enqueue(newPath);
                    }
                }
            }
        }

        return result;
    }
}