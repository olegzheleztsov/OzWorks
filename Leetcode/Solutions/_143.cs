// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _143
{
    public void ReorderList(ListNode head)
    {
        var stack = new Stack<ListNode>();
        var queue = new Queue<ListNode>();

        var ptr = head;
        int totalCount = 0;
        while (ptr != null)
        {
            totalCount++;
            ptr = ptr.next;
        }

        var sp = head;
        for (int i = 0; i <= totalCount / 2; i++)
        {
            sp = sp.next;
        }

        var sentinel = sp;
        if (sentinel == null)
        {
            return;
        }

        ptr = head;
        while (ptr != sentinel)
        {
            queue.Enqueue(ptr);
            ptr = ptr.next;
        }

        while (sp != null)
        {
            stack.Push(sp);
            sp = sp.next;
        }

        ListNode p = null;
        while (stack.Count > 0)
        {

            var n1 = queue.Dequeue();
            var n2 = stack.Pop();

            if (p == null)
            {
                p = n1;
            }
            else
            {
                p.next = n1;
                p = p.next;
            }

            p.next = n2;
            p = p.next;
        }

        while (queue.Count > 0)
        {
            var n = queue.Dequeue();
            p.next = n;
            p = p.next;
        }

        p.next = null;
    }
}