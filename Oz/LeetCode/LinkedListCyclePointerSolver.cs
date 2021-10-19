#region

using System;

#endregion

namespace Oz.LeetCode;

public class LinkedListCyclePointerSolver
{
    private bool HasCycle(ListNode head)
    {
        if (head == null)
        {
            return false;
        }

        var fastPointer = head;
        var slowPointer = head;

        do
        {
            slowPointer = slowPointer.Next;
            if (slowPointer == null)
            {
                return false;
            }

            fastPointer = fastPointer.Next;
            if (fastPointer == null)
            {
                return false;
            }

            fastPointer = fastPointer.Next;
            if (fastPointer == null)
            {
                return false;
            }
        } while (slowPointer != fastPointer);

        return true;
    }

    public ListNode DetectCycle(ListNode head)
    {
        if (head == null)
        {
            return null;
        }

        var fastPointer = head;
        var slowPointer = head;

        do
        {
            slowPointer = slowPointer.Next;
            if (slowPointer == null)
            {
                return null;
            }

            fastPointer = fastPointer.Next;
            if (fastPointer == null)
            {
                return null;
            }

            fastPointer = fastPointer.Next;
            if (fastPointer == null)
            {
                return null;
            }
        } while (slowPointer != fastPointer);

        var slowPointer2 = head;

        while (slowPointer != slowPointer2)
        {
            slowPointer = slowPointer.Next;
            slowPointer2 = slowPointer2.Next;
        }

        return slowPointer;
    }


    public static void Test()
    {
        TestInner1();
        TestInner2();
        TestInner3();
        TestInner4();
        TestInner5();
    }

    private static void TestInner5()
    {
        var n1 = new ListNode(1);
        var n2 = new ListNode(2);
        n1.Next = n2;
        n2.Next = n1;
        var solver = new LinkedListCyclePointerSolver();
        Console.WriteLine(solver.DetectCycle(n1).Val);
    }

    private static void TestInner4()
    {
        var n3 = new ListNode(3);
        var n2 = new ListNode(2);
        var n0 = new ListNode();
        var n4 = new ListNode(-4);

        n3.Next = n2;
        n2.Next = n0;
        n0.Next = n4;
        n4.Next = n2;

        var solver = new LinkedListCyclePointerSolver();
        Console.WriteLine(solver.DetectCycle(n3).Val);
    }

    private static void TestInner1()
    {
        var n3 = new ListNode(3);
        var n2 = new ListNode(2);
        var n0 = new ListNode();
        var n4 = new ListNode(-4);

        n3.Next = n2;
        n2.Next = n0;
        n0.Next = n4;
        n4.Next = n2;

        var solver = new LinkedListCyclePointerSolver();
        Console.WriteLine(solver.HasCycle(n3));
    }

    private static void TestInner2()
    {
        var n1 = new ListNode(1);
        var n2 = new ListNode(2);
        n1.Next = n2;
        n2.Next = n1;
        var solver = new LinkedListCyclePointerSolver();
        Console.WriteLine(solver.HasCycle(n1));
    }

    private static void TestInner3()
    {
        var n1 = new ListNode(1);
        var solver = new LinkedListCyclePointerSolver();
        Console.WriteLine(solver.HasCycle(n1));
    }
}