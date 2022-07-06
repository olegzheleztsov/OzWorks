// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _142
{
    public ListNode DetectCycle(ListNode head)
    {
        if (head is null)
        {
            return null;
        }

        var slowPointer = head;
        var fastPointer = head;

        do
        {
            slowPointer = slowPointer.next;
            fastPointer = fastPointer.next;
            if (fastPointer == null)
            {
                return null;
            }

            fastPointer = fastPointer.next;
            if (fastPointer == null)
            {
                return null;
            }
        } while (slowPointer != fastPointer);

        var loopNodes = new HashSet<ListNode>();
        while (true)
        {
            if (loopNodes.Contains(slowPointer))
            {
                break;
            }

            loopNodes.Add(slowPointer);
            slowPointer = slowPointer.next;
        }

        var pointer = head;
        while (true)
        {
            if (loopNodes.Contains(pointer))
            {
                return pointer;
            }

            pointer = pointer.next;
        }
    }
}