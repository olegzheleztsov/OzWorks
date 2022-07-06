// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _138
{
    public class Node {
        public int val;
        public Node next;
        public Node random;
    
        public Node(int _val) {
            val = _val;
            next = null;
            random = null;
        }
    }
    
    public Node CopyRandomList(Node head)
    {
        if (head == null)
        {
            return null;
        }
        var p = head;

        while (p != null)
        {
            var newNode = new Node(p.val)
            {
                next = p.next,
                random = p.random
            };
            p.next = newNode;
            p = newNode.next;
        }

        p = head;
        while (p != null)
        {
            p = p.next;
            if (p.random != null)
            {
                p.random = p.random.next;
            }

            p = p.next;
        }


        var newHead = head.next;

        p = head;
        var pn = newHead;

        while (p != null)
        {
            p.next = pn.next;
            p = p.next;
            if (p != null)
            {
                pn.next = p.next;
                pn = pn.next;
            }
            else
            {
                pn.next = null;
            }
        }

        return newHead;
    }
}