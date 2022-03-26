// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet83
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    
    public ListNode DeleteDuplicates(ListNode head) {
        if (head?.next == null)
        {
            return head;
        }

        var prev = head;
        var curr = head.next;

        while (curr != null)
        {
            if (curr.val == prev.val)
            {
                prev.next = curr.next;
                curr = curr.next;
            }
            else
            {
                prev = curr;
                curr = curr.next;
            }
            
        }

        return head;
    }
}