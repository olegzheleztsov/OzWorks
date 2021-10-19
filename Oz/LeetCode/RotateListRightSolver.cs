#region

using System;

#endregion

namespace Oz.LeetCode;

public static class RotateListRightSolver
{
    private static ListNode RotateRight(ListNode head, int k)
    {
        var counter = 0;
        var temp = head;
        ListNode last = null;
        while (temp != null)
        {
            counter++;
            last = temp;
            temp = temp.Next;
        }

        if (counter == 0)
        {
            return head;
        }

        var shiftSize = k % counter;
        if (shiftSize == 0)
        {
            return head;
        }

        ListNode prev = null;
        temp = head;
        var newHeadIndex = counter - shiftSize;
        var index = 0;
        while (index < newHeadIndex)
        {
            prev = temp;
            temp = temp.Next;
            index++;
        }

        if (last != null)
        {
            last.Next = head;
        }

        head = temp;
        if (prev != null)
        {
            prev.Next = null;
        }

        return head;
    }

    private static void PrintList(ListNode head)
    {
        var temp = head;
        while (temp != null)
        {
            Console.Write($"{temp.Val} ");
            temp = temp.Next;
        }

        Console.WriteLine();
    }

    public static void Test()
    {
        var n5 = new ListNode(5);
        var n4 = new ListNode(4, n5);
        var n3 = new ListNode(3, n4);
        var n2 = new ListNode(2, n3);
        var n1 = new ListNode(1, n2);
        var head = n1;
        var r = RotateRight(head, 1);
        PrintList(r);
    }
}