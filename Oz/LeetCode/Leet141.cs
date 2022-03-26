// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet141
{
    public bool HasCycle(ListNode head)
    {
        HashSet<ListNode> nodes = new HashSet<ListNode>();

        var pointer = head;

        while (pointer != null)
        {
            if (nodes.Contains(pointer))
            {
                return true;
            }

            nodes.Add(pointer);
            pointer = pointer.Next;
        }

        return false;
    }
}