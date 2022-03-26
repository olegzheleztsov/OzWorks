// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet206
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    
    public ListNode ReverseList(ListNode head) {
        
        if (head?.next == null)
        {
            return head;
        }

        var prev = head;
        var curr = head.next;

        while (curr != null)
        {
            var temp = curr.next;
            curr.next = prev;
            if (prev == head)
            {
                prev.next = null;
            }
            prev = curr;
            curr = temp;
        }

        return prev;
    }
}