// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

public class Leet127
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        var queue = new Queue<string>();
        var visited = new HashSet<string>();
        var bank = new HashSet<string>();
        queue.Enqueue(beginWord);
        var moves = 0;

        while (queue.Count > 0)
        {
            var cnt = queue.Count;
            for (var i = 0; i < cnt; i++)
            {
                var current = queue.Dequeue();
                if (current == endWord)
                {
                    return moves;
                }

                var candidates = wordList.Where(w => !visited.Contains(w) && Difference(current, w) == 1);

                foreach (var candidate in candidates)
                {
                    visited.Add(candidate);
                    queue.Enqueue(candidate);
                }
            }

            moves++;
        }

        return 0;
    }

    private int Difference(string word1, string word2)
    {
        var cnt = 0;
        for (var i = 0; i < word1.Length; i++)
        {
            if (word1[i] != word2[i])
            {
                cnt++;
            }
        }

        return cnt;
    }
}