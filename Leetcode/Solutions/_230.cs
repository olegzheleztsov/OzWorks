// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _230
{
    public int KthSmallest(TreeNode root, int k)
    {
        int res = 0;
        if (root == null)
        {
            return res;
        }

        Dfs(root, 1, k, ref res);
        return res;
    }

    private int Dfs(TreeNode node, int i, int k, ref int res)
    {
        int cur = node.left == null ? i : Dfs(node.left, i, k, ref res) + 1;
        if (cur == k)
        {
            res = node.val;
        }

        return node.right == null ? cur : Dfs(node.right, cur + 1, k, ref res);
    }
}