// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Models;

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