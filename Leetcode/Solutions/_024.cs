// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _024
{
    public ListNode SwapPairs(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }

        ListNode pPrev = null;
        var pCurrent = head;
        var pNext = head.next;
        var newHead = head.next;

        while (pNext != null)
        {
            var savedNext = pNext.next;
            pCurrent.next = savedNext;
            pNext.next = pCurrent;
            if (pPrev != null)
            {
                pPrev.next = pNext;
            }

            pPrev = pCurrent;
            pCurrent = savedNext;
            pNext = savedNext?.next;
        }

        return newHead;
    }
}