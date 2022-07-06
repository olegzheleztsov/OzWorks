// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Collections.Generic;

public class Leet752
{
    public static void Test()
    {
        string[] deadends = {"0201", "0101", "0102", "1212", "2002"};
        var target = "0202";
        var obj = new Leet752();
        var count = obj.OpenLock(deadends, target);
        Console.WriteLine(count);
    }

    public int OpenLock(string[] deadends, string target)
    {
        var visited = new HashSet<string>();
        var deadSet = new HashSet<string>(deadends);
        var queue = new Queue<string>();

        if (deadSet.Contains("0000"))
        {
            return -1;
        }

        queue.Enqueue("0000");

        var moves = 0;

        while (queue.Count > 0)
        {
            var cnt = queue.Count;

            for (var i = 0; i < cnt; i++)
            {
                var current = queue.Dequeue();
                if (current == target)
                {
                    return moves;
                }

                for (var j = 0; j < 4; j++)
                {
                    var candidateRight = Move(current, j, true);
                    var candidateLeft = Move(current, j, false);

                    if (!visited.Contains(candidateRight) && !deadSet.Contains(candidateRight))
                    {
                        visited.Add(candidateRight);
                        queue.Enqueue(candidateRight);
                    }

                    if (!visited.Contains(candidateLeft) && !deadSet.Contains(candidateLeft))
                    {
                        visited.Add(candidateLeft);
                        queue.Enqueue(candidateLeft);
                    }
                }
            }

            moves++;
        }

        return -1;
    }

    private string Move(string s, int index, bool isForward)
    {
        var charr = s.ToCharArray();
        var num = int.Parse(charr[index].ToString());

        if (isForward)
        {
            if (num == 9)
            {
                num = 0;
            }
            else
            {
                num++;
            }
        }
        else
        {
            if (num == 0)
            {
                num = 9;
            }
            else
            {
                num--;
            }
        }

        charr[index] = num.ToString()[0];
        return new string(charr);
    }
}