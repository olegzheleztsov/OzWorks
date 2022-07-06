// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _2
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {

        int len1 = 0;
        int len2 = 0;

        var p = l1;
        while (p != null)
        {
            len1++;
            p = p.next;
        }

        p = l2;
        while (p != null)
        {
            len2++;
            p = p.next;
        }

        var first = len1 >= len2 ? l1 : l2;
        var second = len1 < len2 ? l1 : l2;
        int memo = 0;

        var pFirst = first;
        var pSecond = second;
        ListNode headResult = null;
        ListNode pResult = null;

        while (pFirst != null)
        {
            int val = 0;
            int n = 0;

            if (pSecond != null)
            {
                val = pSecond.val + pFirst.val + memo;
            }
            else
            {
                val = pFirst.val + memo;
            }

            memo = val / 10;
            n = val % 10;
            var newNode = new ListNode(n);
            if (pResult != null)
            {
                pResult.next = newNode;
                pResult = newNode;
            }
            else
            {
                pResult = newNode;
            }

            if (headResult == null)
            {
                headResult = pResult;
            }

            if (pSecond != null)
            {
                pSecond = pSecond.next;
            }

            pFirst = pFirst.next;
        }

        if (memo != 0)
        {
            if (pResult != null)
            {
                pResult.next = new ListNode(memo);
            }
        }

        return headResult;
    }
}