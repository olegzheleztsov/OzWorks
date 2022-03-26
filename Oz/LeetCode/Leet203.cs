// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet203
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    
    public ListNode RemoveElements(ListNode head, int val) {

        while (head != null)
        {
            if (head.val != val)
            {
                break;
            }
            else
            {
                head = head.next;
            }
        }

        var prev = head;
        var curr = head.next;
        while (curr != null)
        {
            if (curr.val == val)
            {
                prev.next = curr.next;
                curr = prev.next;
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