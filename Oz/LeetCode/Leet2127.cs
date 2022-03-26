// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet2127
{
    public static void Test()
    {
        Leet2127 obj = new Leet2127();
        obj.MaximumInvitations(new int[] {2, 2, 1, 2});
    }
    
    int[] inDegrees = null;
    int len = 0;
    
    public int MaximumInvitations(int[] favorite)
    {
        len = favorite.Length;

        int[] level = new int[len];
        bool[] visited = new bool[len];
        TopSort(favorite, level, visited);

        int maxCycle = 0;
        int maxLinear = 0;

        for (int i = 0; i < len; i++)
        {
            if (!visited[i])
            {
                if (favorite[favorite[i]] == i)
                {
                    maxLinear += level[i] + 1;
                }
                else
                {
                    maxCycle = Math.Max(maxCycle, CountCycle(favorite, i, visited));
                }
            }
        }

        return Math.Max(maxCycle, maxLinear);
    }

    void TopSort(int[] favorite, int[] level, bool[] visited)
    {
        inDegrees = new int[len];
        for (int i = 0; i < len; i++)
        {
            inDegrees[favorite[i]]++;
        }

        var q = new Queue<int>();
        for (int i = 0; i < len; i++)
        {
            if (inDegrees[i] == 0)
            {
                q.Enqueue(i);
            }
        }

        int lvl = 0;
        while (q.Count > 0)
        {
            int count = q.Count;
            lvl++;

            while (count > 0 )
            {
                var cur = q.Dequeue();
                visited[cur] = true;
                var next = favorite[cur];
                inDegrees[next]--;
                level[next] = lvl;
                if (inDegrees[next] == 0)
                {
                    q.Enqueue(next);
                }

                count--;
            }
        }
    }

    int CountCycle(int[] favorite, int start, bool[] visited)
    {
        int count = 0;
        var cur = start;

        while (true)
        {
            if (visited[cur])
            {
                break;
            }

            visited[cur] = true;
            cur = favorite[cur];
            count++;
        }

        return count;
    }
}