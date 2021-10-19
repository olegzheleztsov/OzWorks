// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet_1319
{
    public static void Test()
    {
        int[][] connections = {new[] {0, 1}, new[] {0, 2}, new[] {1, 2}};
        var obj = new Leet_1319();
        obj.MakeConnected(4, connections);
    }

    public int MakeConnected(int n, int[][] connections)
    {
        List<int> notVisited = new();
        List<int> visited = new();
        var countOfExtra = 0;

        var computers = new Computer[n];

        foreach (var connection in connections)
        {
            if (computers[connection[0]] == null)
            {
                computers[connection[0]] = new Computer {Index = connection[0]};
            }

            computers[connection[0]].Connected.Add(connection[1]);

            if (computers[connection[1]] == null)
            {
                computers[connection[1]] = new Computer {Index = connection[1]};
                computers[connection[1]].Connected.Add(connection[0]);
            }
        }

        for (var i = 0; i < computers.Length; i++)
        {
            if (computers[i] == null)
            {
                computers[i] = new Computer {Index = i};
            }
        }

        Dfs(computers, out var busyConnections, out var networkCount);

        var freeDiff = connections.Length - busyConnections;
        if (freeDiff >= networkCount - 1)
        {
            return networkCount - 1;
        }

        return -1;
    }

    private void Dfs(Computer[] computers, out int countOfRequiredConnections, out int countOfNetworks)
    {
        countOfRequiredConnections = 0;
        countOfNetworks = 0;

        Stack<Computer> stack = new();


        var compsInNetwork = 0;

        while (computers.Any(c => !c.Discovered))
        {
            stack.Push(computers.First(c => !c.Discovered));
            compsInNetwork = 1;

            while (stack.Count > 0)
            {
                var comp = stack.Pop();
                if (!comp.Discovered)
                {
                    comp.Discovered = true;
                    foreach (var adjInd in comp.Connected)
                    {
                        if (!computers[adjInd].Discovered)
                        {
                            stack.Push(computers[adjInd]);
                            compsInNetwork++;
                        }
                    }
                }
            }

            countOfRequiredConnections += compsInNetwork - 1;
            countOfNetworks++;
        }
    }

    public class Computer
    {
        public int Index { get; set; }

        public bool Discovered { get; set; }
        public HashSet<int> Connected { get; } = new();
    }
}