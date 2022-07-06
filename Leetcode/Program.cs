using Leetcode.Models;
using Leetcode.Solutions;

_826.Test();

void RotateRightTest()
{
    var n1 = new ListNode(1);
    var n2 = new ListNode(2);
    n1.next = n2;
    var sut = new _61();
    sut.RotateRight(n1, 0);
}

void CheckListAdditions()
{
    var n9 = GenerateArrayOf9(7);
    var n4 = GenerateArrayOf9(4);
    var sut = new _2();
    var result = sut.AddTwoNumbers(n9, n4);
}

ListNode GenerateArrayOf9(int len)
{
    var nodes = new ListNode[len];
    for (var i = 0; i < len; i++)
    {
        nodes[i] = new ListNode(9);
    }

    for (var i = 0; i < len - 1; i++)
    {
        nodes[i].next = nodes[i + 1];
    }

    return nodes[0];
}