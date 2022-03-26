// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet876
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }

    public static void Test()
    {
        ListNode n1 = new ListNode(1);
        ListNode n2 = new ListNode(2);
        ListNode n3 = new ListNode(3);
        ListNode n4 = new ListNode(4);
        ListNode n5 = new ListNode(5);
        n1.next = n2;
        n2.next = n3;
        n3.next = n4;
        n4.next = n5;

        var app = new Leet876();
        app.MiddleNode(n1);
    
    }
    
    public ListNode MiddleNode(ListNode head)
    {

        if (head.next == null)
        {
            return head;
        }

        if (head.next.next == null)
        {
            return head.next;
        }
        
        var slowPointer = head;
        var fastPointer = head;
        bool advance = false;

        while (fastPointer != null && fastPointer.next != null)
        {
            slowPointer = slowPointer.next;
            fastPointer = fastPointer.next;
            if (fastPointer == null)
            {
                advance = true;
                break;
            }

            fastPointer = fastPointer.next;
        }

        if (advance)
        {
            slowPointer = slowPointer.next;
        }

        return slowPointer;
    }
}