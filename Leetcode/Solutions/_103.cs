// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _103
{
    public void Test()
    {
        var root = new TreeNode(1);
        var t2 = new TreeNode(2);
        var t3 = new TreeNode(3);
        var t4 = new TreeNode(4);
        var t5 = new TreeNode(5);
        root.left = t2;
        root.right = t3;
        t2.left = t4;
        t2.right = t5;
        var result = ZigzagLevelOrder(root);
    }

    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        if (root == null)
        {
            return new List<IList<int>>();
        }

        var result = new List<IList<int>>();
        var isLeftToRight = true;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        var stack = new Stack<TreeNode>();

        while (queue.Count > 0)
        {
            var lst = new List<int>();
            var cnt = queue.Count;
            for (var i = 0; i < cnt; i++)
            {
                var nd = queue.Dequeue();
                lst.Add(nd.val);
                if (isLeftToRight)
                {
                    if (nd.left != null)
                    {
                        stack.Push(nd.left);
                    }

                    if (nd.right != null)
                    {
                        stack.Push(nd.right);
                    }
                }
                else
                {
                    if (nd.right != null)
                    {
                        stack.Push(nd.right);
                    }

                    if (nd.left != null)
                    {
                        stack.Push(nd.left);
                    }
                }
            }

            isLeftToRight = !isLeftToRight;
            result.Add(lst);
            while (stack.Count > 0)
            {
                queue.Enqueue(stack.Pop());
            }
        }

        return result;
    }
}