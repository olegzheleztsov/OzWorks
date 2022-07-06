// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1823
{
    public void Test()
    {
        var num = FindTheWinner(5, 2);
    }

    public int FindTheWinner(int n, int k)
    {
        if (n == 1)
        {
            return 1;
        }

        if (k == 1)
        {
            return n;
        }

        var head = new Node
        {
            Val = 1
        };

        var prev = head;

        for (var i = 2; i <= n; i++)
        {
            var nd = new Node
            {
                Val = i
            };
            prev.Next = nd;
            prev = nd;
        }

        prev.Next = head;

        while (head.Next != head)
        {
            var p = head;
            var c = 1;

            while (c < k)
            {
                prev = p;
                p = p.Next;
                c++;
            }

            prev.Next = p.Next;
            head = p.Next;
        }

        return head.Val;
    }

    public class Node
    {
        public Node Next;
        public int Val;
    }
}