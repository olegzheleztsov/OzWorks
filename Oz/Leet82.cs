// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public class Leet82
{
    public void Test()
    {
        //[1,2,3,3,4,4,5]
        var n1 = new ListNode(1);
        var n2 = new ListNode(2);
        var n3_1 = new ListNode(3);
        var n3_2 = new ListNode(3);
        var n4_1 = new ListNode(4);
        var n4_2 = new ListNode(4);
        var n5 = new ListNode(5);
        n1.next = n2;
        n2.next = n3_1;
        n3_1.next = n3_2;
        n3_2.next = n4_1;
        n4_1.next = n4_2;
        n4_2.next = n5;
        var result = DeleteDuplicates(n1);
    }


    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return head;
        }

        var prev = head;
        var cur = head.next;
        var deleteVal = new HashSet<int>();
        while (cur != null)
        {
            if (prev.val == cur.val)
            {
                prev.next = cur.next;
                if (!deleteVal.Contains(prev.val))
                {
                    deleteVal.Add(prev.val);
                }
            }
            else
            {
                prev = cur;
            }

            cur = cur.next;
        }

        while (head != null && deleteVal.Contains(head.val))
        {
            head = head.next;
        }

        if (head == null)
        {
            return head;
        }

        prev = head;
        cur = head.next;
        while (cur != null)
        {
            if (deleteVal.Contains(cur.val))
            {
                prev.next = cur.next;
            }
            else
            {
                prev = cur;
            }

            cur = cur.next;
        }


        return head;
    }

    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}