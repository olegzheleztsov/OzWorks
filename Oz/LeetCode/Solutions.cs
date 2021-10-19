using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Solutions
{
    public IList<string> InvalidTransactions(string[] transactions)
    {
        var transactionList = transactions.Select(Transaction.Parse).ToList();
        transactionList.Sort((a, b) => a.Time.CompareTo(b.Time));
        var invalidIndices = new HashSet<Transaction>();

        for (var i = 0; i < transactionList.Count; i++)
        {
            for (var j = i + 1; j < transactionList.Count; j++)
            {
                if (transactionList[j].City != transactionList[i].City &&
                    transactionList[i].Name == transactionList[j].Name)
                {
                    if (transactionList[j].Time - transactionList[i].Time <= 60)
                    {
                        invalidIndices.Add(transactionList[i]);
                        invalidIndices.Add(transactionList[j]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (transactionList[i].Amount > 1000)
            {
                invalidIndices.Add(transactionList[i]);
            }
        }

        return invalidIndices.Select(t => t.ToString()).ToList();
    }

    public int NumRookCaptures(char[][] board)
    {
        int rRow = -1, rCol = -1;

        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (board[i][j] == 'R')
                {
                    rRow = i;
                    rCol = j;
                    break;
                }
            }
        }

        var pawnCounts = 0;

        var index = rCol - 1;
        while (index >= 0)
        {
            if (board[rRow][index] == 'B')
            {
                break;
            }

            if (board[rRow][index] == 'p')
            {
                pawnCounts++;
                break;
            }

            index--;
        }

        index = rCol + 1;
        while (index < 8)
        {
            if (board[rRow][index] == 'B')
            {
                break;
            }

            if (board[rRow][index] == 'p')
            {
                pawnCounts++;
                break;
            }

            index++;
        }

        index = rRow - 1;
        while (index >= 0)
        {
            if (board[index][rCol] == 'B')
            {
                break;
            }

            if (board[index][rCol] == 'p')
            {
                pawnCounts++;
                break;
            }

            index--;
        }

        index = rRow + 1;
        while (index < 8)
        {
            if (board[index][rCol] == 'B')
            {
                break;
            }

            if (board[index][rCol] == 'p')
            {
                pawnCounts++;
                break;
            }

            index++;
        }

        return pawnCounts;
    }

    /// <summary>
    ///     https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1215/
    /// </summary>
    private ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        var cachedElements = new HashSet<ListNode>();
        var pA = headA;
        var pB = headB;
        while (pA != null)
        {
            cachedElements.Add(pA);
            pA = pA.Next;
        }

        while (pB != null)
        {
            if (cachedElements.Contains(pB))
            {
                return pB;
            }

            pB = pB.Next;
        }

        return null;
    }

    /// <summary>
    ///     https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1296/
    /// </summary>
    private ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        var pointer = head;
        var index = 0;
        while (pointer != null && index < n)
        {
            pointer = pointer.Next;
            index++;
        }

        if (pointer == null && index < n)
        {
            return null;
        }

        if (pointer == null && index == n)
        {
            head = head.Next;
            return head;
        }

        var secondPointer = head;
        while (pointer is {Next: { }})
        {
            pointer = pointer.Next;
            secondPointer = secondPointer.Next;
        }

        var removed = secondPointer.Next;
        if (secondPointer.Next != null)
        {
            secondPointer.Next = secondPointer.Next.Next;
        }

        if (removed == head)
        {
            head = head.Next;
        }

        return head;
    }

    private void ReverseList(ListNode head)
    {
        if (head == null)
        {
            return;
        }

        ListNode prevNode = null;
        var pointer = head;
        while (pointer.Next != null)
        {
            var temp = pointer.Next;
            pointer.Next = prevNode;
            prevNode = pointer;
            pointer = temp;
        }

        pointer.Next = prevNode;
    }

    private ListNode RemoveElements(ListNode head, int val)
    {
        if (head == null)
        {
            return null;
        }

        while (head != null && head.Val == val)
        {
            head = head.Next;
        }

        if (head?.Next == null)
        {
            return head;
        }

        var previousNode = head;
        var currentNode = head.Next;
        while (currentNode != null)
        {
            if (currentNode.Val == val)
            {
                previousNode.Next = currentNode.Next;
            }
            else
            {
                previousNode = currentNode;
            }

            currentNode = currentNode.Next;
        }

        return head;
    }

    private ListNode OddEvenList(ListNode head)
    {
        if (head?.Next?.Next == null)
        {
            return head;
        }

        ListNode evenHead = head, evenTail = head;
        ListNode oddHead = null, oddTail = null;

        var pointer = evenHead;
        while (pointer?.Next != null)
        {
            if (oddHead == null)
            {
                oddHead = oddTail = pointer.Next;
            }
            else
            {
                oddTail.Next = pointer.Next;
                oddTail = oddTail.Next;
            }

            if (pointer.Next.Next != null)
            {
                evenTail = pointer.Next.Next;
            }

            pointer.Next = pointer.Next.Next;
            pointer = pointer.Next;

            oddTail.Next = null;
        }

        if (evenTail != null && oddHead != null)
        {
            evenTail.Next = oddHead;
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
        for (var i = 1; i < values.Length; i++)
        {
            var cur = new ListNode(values[i]);
            prev.Next = cur;
            prev = cur;
        }

        return start;
    }

    public void TestReverseList()
    {
        var n1 = new ListNode(1);
        var n2 = new ListNode(2);
        var n3 = new ListNode(3);
        var n4 = new ListNode(4);
        var n5 = new ListNode(5);
        n1.Next = n2;
        n2.Next = n3;
        n3.Next = n4;
        n4.Next = n5;
        ReverseList(n1);
        PrintList(n5);
    }

    private void PrintList(ListNode node)
    {
        Console.Write("[");
        while (node != null)
        {
            Console.Write($"{node.Val}, ");
            node = node.Next;
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
        n1.Next = n2;
        n2.Next = n3;
        n3.Next = n4;
        n4.Next = n5;

        PrintList(RemoveNthFromEnd(n1, 2));

        n1.Next = null;
        PrintList(RemoveNthFromEnd(n1, 1));

        n1.Next = n2;
        n2.Next = null;
        PrintList(RemoveNthFromEnd(n1, 1));

        n1.Next = n2;
        n2.Next = null;
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
        c1.Next = c2;
        c2.Next = c3;
        a1.Next = a2;
        a2.Next = c1;
        b1.Next = b2;
        b2.Next = b3;
        b3.Next = c1;

        var intersectionNode = GetIntersectionNode(a1, b1);
        Console.WriteLine($"intersection value: {intersectionNode.Val}");
    }

    public class Transaction
    {
        private string _raw;
        public string Name { get; private init; }
        public int Time { get; private init; }
        public int Amount { get; private init; }
        public string City { get; private init; }

        public static Transaction Parse(string str)
        {
            var tokens = str.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return new Transaction
            {
                Name = tokens[0],
                Time = int.Parse(tokens[1]),
                Amount = int.Parse(tokens[2]),
                City = tokens[3],
                _raw = str
            };
        }

        public override string ToString() => _raw;
    }

    public class ListNode
    {
        public ListNode Next;
        public readonly int Val;

        public ListNode(int x) =>
            Val = x;
    }
}