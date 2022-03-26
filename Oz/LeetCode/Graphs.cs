// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet365
{
    public bool CanMeasureWater(int jug1Capacity, int jug2Capacity, int targetCapacity) {

        if (targetCapacity == 0)
        {
            return true;
        }
        
        if(targetCapacity > jug1Capacity + jug2Capacity ) {
            return false;
        }
        
        var gcd = Gcd(jug1Capacity, jug2Capacity);
        if (targetCapacity % gcd == 0)
        {
            return true;
        }

        return false;
        
        int Gcd(int x, int y)
        {
            if (y == 0)
            {
                return x;
            }

            return Gcd(y, x % y);
        }
    }
}

public class Leet1654
{
    public int MinimumJumps(int[] forbidden, int a, int b, int x)
    {
        HashSet<int> blocked = new HashSet<int>(forbidden);
        HashSet<(int, bool)> visited = new HashSet<(int, bool)>();
        Queue<(int, bool)> q = new Queue<(int, bool)>();

        q.Enqueue((0, false));
        visited.Add((0, false));
        int result = 0;
        int maxForbidden = forbidden.Max();
        int max = Math.Max(x, maxForbidden) + 2 * a + b + 1;

        while(q.Count > 0)
        {
            int count = q.Count;
            for(int i = 0; i < count; i++)
            {
                (int current, bool isBack) = q.Dequeue();                
                if(current == x)
                {
                    return result;
                }

                int forward = current + a;
                int backward = current - b;

                if(forward > 0 && forward < max && !blocked.Contains(forward) && !visited.Contains((forward, false)))
                {
                    visited.Add((forward, false));
                    q.Enqueue((forward, false));
                }

                if(!isBack && backward > 0 && backward < max && !blocked.Contains(backward) && !visited.Contains((backward, true)))
                {
                    visited.Add((backward, true));
                    q.Enqueue((backward, true));
                }
            }

            result++;
        }

        return -1;
    }


}

public class Leet1306
{
    public bool CanReach(int[] arr, int start)
    {
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var val = queue.Dequeue();
            if (arr[val] == 0)
            {
                return true;
            }

            if (val - arr[val] >= 0)
            {
                if (!visited.Contains(val - arr[val]))
                {
                    queue.Enqueue(val - arr[val]);
                    visited.Add(val - arr[val]);
                }
            }

            if (val + arr[val] < arr.Length)
            {
                if (!visited.Contains(val + arr[val]))
                {
                    queue.Enqueue(val + arr[val]);
                    visited.Add(val + arr[val]);
                }
            }
        }

        return false;
    }
}

public class Leet847
{
    public int ShortestPathLength(int[][] graph)
    {
        var n = graph.Length;
        var used = 0;
        var queue = new Queue<Point>();
        var visitedStates = new HashSet<Point>();

        for (var i = 0; i < n; i++)
        {
            used |= 1 << i;
            queue.Enqueue(new Point(i, used));
            visitedStates.Add(new Point(i, used));
            used ^= 1 << i;
        }

        var depth = 0;
        while (queue.Count > 0)
        {
            depth++;
            for (var size = queue.Count; size > 0; size--)
            {
                var item = queue.Dequeue();
                var curr = item.X;
                used = item.Y;

                foreach (var child in graph[curr])
                {
                    var x = used;
                    var next = new Point(child, used);
                    if (!visitedStates.Contains(next))
                    {
                        visitedStates.Add(next);
                        used |= 1 << child;
                        if (used == (int)Math.Pow(2, n) - 1)
                        {
                            return depth;
                        }

                        queue.Enqueue(new Point(child, used));
                        used = x;
                    }
                }
            }
        }

        return 0;
    }

    public record Point(int X, int Y);
}