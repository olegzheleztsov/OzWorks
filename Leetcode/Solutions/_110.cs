// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _110
{
    public bool IsBalanced(TreeNode root)
    {
        if (root == null)
        {
            return true;
        }

        if (root.left == null && root.right == null)
        {
            return true;
        }

        var leftIsBalanced = IsBalanced(root.left);
        var rightIsBalanced = IsBalanced(root.right);

        if (!leftIsBalanced || !rightIsBalanced)
        {
            return false;
        }

        var leftHeight = GetHeight(root.left);
        var rightHeight = GetHeight(root.right);
        return Math.Abs(leftHeight - rightHeight) < 2;
    }

    private int GetHeight(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        return Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;
    }
}