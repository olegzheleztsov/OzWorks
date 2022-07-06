// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _543
{
    public int DiameterOfBinaryTree(TreeNode root)
    {
        var result = 0;
        Dfs(root, ref result);
        return result;
    }

    private int Dfs(TreeNode node, ref int result)
    {
        if (node == null)
        {
            return -1;
        }

        var leftMaxDepth = Dfs(node.left, ref result);
        var rightMaxDepth = Dfs(node.right, ref result);
        result = Math.Max(leftMaxDepth + rightMaxDepth + 2, result);
        return Math.Max(leftMaxDepth, rightMaxDepth) + 1;
    }
}