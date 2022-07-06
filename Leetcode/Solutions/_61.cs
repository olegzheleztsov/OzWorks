// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _61
{
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null)
        {
            return null;
        }



        var length = GetListLength(head);

        k %= length;

        if (k == 0)
        {
            return head;
        }
        
        var tail = GetTailNode(head);
        var p = head;
        ListNode pPrev = null;
        for (var i = 0; i < length - k; i++)
        {
            if (p.next != null)
            {
                pPrev = p;
                p = p.next;
            }
        }

        if (pPrev != null)
        {
            pPrev.next = null;
        }

        tail.next = tail != head ? head : null;

        return p;
    }

    private int GetListLength(ListNode head)
    {
        var p = head;
        var count = 0;
        while (p != null)
        {
            count++;
            p = p.next;
        }

        return count;
    }

    private ListNode GetTailNode(ListNode head)
    {
        var p = head;
        while (p.next != null)
        {
            p = p.next;
        }

        return p;
    }
}