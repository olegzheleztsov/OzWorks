using System.Collections.Generic;

namespace Oz.LeetCode;

public class OpenLockSolver
{
    public int OpenLock(string[] deadends, string target)
    {
        var length = 0;
        var queue = new Queue<string>();
        var deadEnds = new HashSet<string>(deadends);

        queue.Enqueue("0000");
        while (queue.Count > 0)
        {
            var queSize = queue.Count;
            for (var k = 0; k < queSize; k++)
            {
                var currentElement = queue.Dequeue();
                if (deadEnds.Contains(currentElement))
                {
                    continue;
                }

                if (currentElement == target)
                {
                    return length;
                }

                deadEnds.Add(currentElement);

                for (var i = 0; i < 4; i++)
                {
                    var candidateUp = Move(currentElement, i, true);
                    var candidateDown = Move(currentElement, i, false);
                    if (!deadEnds.Contains(candidateUp))
                    {
                        queue.Enqueue(candidateUp);
                    }

                    if (!deadEnds.Contains(candidateDown))
                    {
                        queue.Enqueue(candidateDown);
                    }
                }
            }

            length++;
        }

        return -1;
    }

    private static string Move(string sourceString, int atIndex, bool isUp)
    {
        var value = int.Parse(sourceString[atIndex].ToString());
        if (isUp)
        {
            value++;
        }
        else
        {
            value--;
        }

        if (value >= 10)
        {
            value = 0;
        }
        else if (value < 0)
        {
            value = 9;
        }

        var s = string.Empty;
        for (var i = 0; i < sourceString.Length; i++)
        {
            s += i == atIndex ? value.ToString() : sourceString[i].ToString();
        }

        return s;
    }
}