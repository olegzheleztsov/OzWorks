#region

using System;

#endregion

namespace Oz.LeetCode
{
    public class LinkedListCyclePointerSolver
    {
        public bool HasCycle(ListNode head)
        {
            if (head == null)
            {
                return false;
            }
            
            var fastPointer = head;
            var slowPointer = head;

            do
            {
                slowPointer = slowPointer.next;
                if (slowPointer == null)
                {
                    return false;
                }

                fastPointer = fastPointer.next;
                if (fastPointer == null)
                {
                    return false;
                }

                fastPointer = fastPointer.next;
                if (fastPointer == null)
                {
                    return false;
                }
            } while (slowPointer != fastPointer);

            return true;
        }
        
        public ListNode DetectCycle(ListNode head) {
            if (head == null)
            {
                return null;
            }
            
            var fastPointer = head;
            var slowPointer = head;

            do
            {
                slowPointer = slowPointer.next;
                if (slowPointer == null)
                {
                    return null;
                }

                fastPointer = fastPointer.next;
                if (fastPointer == null)
                {
                    return null;
                }

                fastPointer = fastPointer.next;
                if (fastPointer == null)
                {
                    return null;
                }
            } while (slowPointer != fastPointer);

            var slowPointer2 = head;

            while (slowPointer != slowPointer2)
            {
                slowPointer = slowPointer.next;
                slowPointer2 = slowPointer2.next;
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
            n1.next = n2;
            n2.next = n1;
            var solver = new LinkedListCyclePointerSolver();
            Console.WriteLine(solver.DetectCycle(n1).val);
        }

        private static void TestInner4()
        {
            var n3 = new ListNode(3);
            var n2 = new ListNode(2);
            var n0 = new ListNode();
            var n_4 = new ListNode(-4);

            n3.next = n2;
            n2.next = n0;
            n0.next = n_4;
            n_4.next = n2;

            var solver = new LinkedListCyclePointerSolver();
            Console.WriteLine(solver.DetectCycle(n3).val);
        }

        private static void TestInner1()
        {
            var n3 = new ListNode(3);
            var n2 = new ListNode(2);
            var n0 = new ListNode();
            var n_4 = new ListNode(-4);

            n3.next = n2;
            n2.next = n0;
            n0.next = n_4;
            n_4.next = n2;

            var solver = new LinkedListCyclePointerSolver();
            Console.WriteLine(solver.HasCycle(n3));
        }

        private static void TestInner2()
        {
            var n1 = new ListNode(1);
            var n2 = new ListNode(2);
            n1.next = n2;
            n2.next = n1;
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
}