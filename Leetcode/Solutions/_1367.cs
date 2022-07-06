// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _1367
{
    public bool IsSubPath(ListNode head, TreeNode root)
    {
        bool isSubPath = false;
        Action<ListNode, TreeNode> visitor = (ln, tn) =>
        {
            if (!isSubPath)
            {
                isSubPath = Check(ln, tn);
            }
        };
        Preorder(head, root, visitor);
        return isSubPath;
    }

    private void Preorder(ListNode head, TreeNode root, Action<ListNode, TreeNode> visit)
    {
        visit(head, root);
        if (root.left != null)
        {
            Preorder(head, root.left, visit);
        }

        if (root.right != null)
        {
            Preorder(head, root.right, visit);
        }
    }

    private bool Check(ListNode head, TreeNode root)
    {
        
        if (head == null)
        {
            return true;
        }

        if (root == null)
        {
            return false;
        }

        return (head.val == root.val) && (Check(head.next, root.left) || Check(head.next, root.right));
    }
}