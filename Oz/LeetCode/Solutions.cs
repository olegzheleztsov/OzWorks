using System;
using System.Collections.Generic;

namespace Oz.LeetCode
{
    public class Solutions
    {
        /// <summary>
        /// https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1215/
        /// </summary>
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var cachedElements = new HashSet<ListNode>();
            var pA = headA;
            var pB = headB;
            while (pA != null )
            {
                cachedElements.Add(pA);
                pA = pA.next;
            }
            
            while (pB != null)
            {
                if (cachedElements.Contains(pB))
                {
                    return pB;
                }

                pB = pB.next;
            }

            return null;
        }
        
        /// <summary>
        /// https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1296/
        /// </summary>
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var pointer = head;
            int index = 0;
            while (pointer != null && index < n)
            {
                pointer = pointer.next;
                index++;
            }
            
            if (pointer == null && index < n)
            {
                return null;
            }

            if (pointer == null && index == n)
            {
                head = head.next;
                return head;
            }
            
            var secondPointer = head;
            while (pointer.next != null)
            {
                pointer = pointer.next;
                secondPointer = secondPointer.next;
            }

            var removed = secondPointer.next;
            if (secondPointer.next != null)
            {
                secondPointer.next = secondPointer.next.next;
            }

            if (removed == head)
            {
                head = head.next;
            }

            return head;
        }
        
        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }
            
            ListNode prevNode = null;
            var pointer = head;
            while (pointer.next != null)
            {
                var temp = pointer.next;
                pointer.next = prevNode;
                prevNode = pointer;
                pointer = temp;
            }

            pointer.next = prevNode;

            return pointer;
        }
        
        public ListNode RemoveElements(ListNode head, int val) {
            if (head == null)
            {
                return null;
            }

            while (head != null && head.val == val)
            {
                head = head.next;
            }

            if (head?.next == null)
            {
                return head;
            }

            var previousNode = head;
            var currentNode = head.next;
            while (currentNode != null)
            {
                if (currentNode.val == val)
                {
                    previousNode.next = currentNode.next;
                }
                else
                {
                    previousNode = currentNode;
                }
                currentNode = currentNode.next;
            }

            return head;
        }
        
        public ListNode OddEvenList(ListNode head)
        {

            if (head?.next?.next == null)
            {
                return head;
            } 
            
            ListNode evenHead = head, evenTail = head;
            ListNode oddHead = null, oddTail = null;

            var pointer = evenHead;
            while (pointer?.next != null)
            {
                if (oddHead == null)
                {
                    oddHead = oddTail = pointer.next;
                }
                else
                {
                    oddTail.next = pointer.next;
                    oddTail = oddTail.next;
                }

                if (pointer.next.next != null)
                {
                    evenTail = pointer.next.next;
                }

                pointer.next = pointer.next.next;
                pointer = pointer.next;
                
                oddTail.next = null;
            }

            if (evenTail != null && oddHead != null)
            {
                evenTail.next = oddHead;
            }

            return evenHead;
        }


        public void TestOddEvenList()
        {
            var list = ConstructList(1, 2, 3, 4, 5);
            var result = OddEvenList(list);
            PrintList(result);

            list = ConstructList(2, 1, 3, 5, 6, 4, 7);
            PrintList(OddEvenList(list));
        }

        public void TestRemoveElements()
        {
            var list = ConstructList(1, 2, 6, 3, 4, 5, 6);
            var result = RemoveElements(list, 6);
            PrintList(result);
        }

        private ListNode ConstructList(params int[] values)
        {
            var prev = new ListNode(values[0]);
            var start = prev;
            for (int i = 1; i < values.Length; i++)
            {
                var cur = new ListNode(values[i]);
                prev.next = cur;
                prev = cur;
            }

            return start;
        }

        public void TestReverseList()
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
            var result = ReverseList(n1);
            PrintList(n5);
        }

        private void PrintList(ListNode node)
        {
            Console.Write("[");
            while (node != null)
            {
                Console.Write($"{node.val}, ");
                node = node.next;
            }
            Console.Write("]");
            Console.WriteLine();
        }

        public void TestRemoveNthFromEnd()
        {
            var n1 = new ListNode(1);
            var n2 = new ListNode(2);
            var n3 = new ListNode(3);
            var n4 = new ListNode(4);
            var n5 = new ListNode(5);
            n1.next = n2;
            n2.next = n3;
            n3.next = n4;
            n4.next = n5;
            
            PrintList(RemoveNthFromEnd(n1, 2));

            n1.next = null;
            PrintList(RemoveNthFromEnd(n1, 1));

            n1.next = n2;
            n2.next = null;
            PrintList(RemoveNthFromEnd(n1, 1));

            n1.next = n2;
            n2.next = null;
            PrintList(RemoveNthFromEnd(n1, 2));
        }

        public void TestGetIntersectionNode()
        {
            var a1 = new ListNode(1);
            var a2 = new ListNode(2);
            var b1 = new ListNode(3);
            var b2 = new ListNode(4);
            var b3 = new ListNode(5);
            var c1 = new ListNode(6);
            var c2 = new ListNode(7);
            var c3 = new ListNode(8);
            c1.next = c2;
            c2.next = c3;
            a1.next = a2;
            a2.next = c1;
            b1.next = b2;
            b2.next = b3;
            b3.next = c1;

            var intersectionNode = GetIntersectionNode(a1, b1);
            Console.WriteLine($"intersection value: {intersectionNode.val}");
        }
        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int x)
            {
                val = x;
            }
        }
    }
}