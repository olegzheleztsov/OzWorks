// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet21
{
public class ListNode {
    public int val;
   public ListNode next;
     public ListNode(int val=0, ListNode next=null) {
            this.val = val;
           this.next = next;
        }
}
    public ListNode MergeTwoLists(ListNode list1, ListNode list2) {

        if (list1 == null)
        {
            return list2;
        }

        if (list2 == null)
        {
            return list1;
        }

        var result = list1.val <= list2.val ? list1 : list2;

        if (list1.val <= list2.val)
        {
            list1 = list1.next;
        }
        else
        {
            list2 = list2.next;
        }

        var pLast = result;

        while (list1 != null && list2 != null)
        {
            var nextNode = (list1.val <= list2.val) ? list1 : list2;
            pLast.next = nextNode;
            pLast = pLast.next;
            if (nextNode == list1)
            {
                list1 = list1.next;
            }
            else
            {
                list2 = list2.next;
            }
        }

        if (list1 != null)
        {
            pLast.next = list1;
        }

        if (list2 != null)
        {
            pLast.next = list2;
        }

        return result;
    }
}