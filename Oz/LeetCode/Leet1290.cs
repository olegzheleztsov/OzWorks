// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1290
{

  public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int val=0, ListNode next=null) {
          this.val = val;
          this.next = next;
      }
  }
 
    public int GetDecimalValue(ListNode head)
    {
        int count = 0;
        int mult = 1;

        var pointer = head;
        while (pointer != null)
        {
            mult *= 2;
            pointer = pointer.next;
        }

        mult /= 2;
        int result = 0;
        pointer = head;
        while (pointer != null)
        {
            if (pointer.val != 0)
            {
                result += mult;
            }

            mult /= 2;
            pointer = pointer.next;
        }

        return result;
    }
}