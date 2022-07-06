// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _105
{
    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        if (preorder == null || preorder.Length == 0 || inorder == null || inorder.Length == 0)
        {
            return null;
        }

        return BuildTree(preorder, 0, preorder.Length - 1, inorder, 0, inorder.Length - 1);
    }

    private static TreeNode BuildTree(int[] preorder, int i, int j, int[] inorder, int k, int l)
    {
        if (i > j || k > l)
        {
            return null;
        }

        var node = new TreeNode(preorder[i]);

        if (i == j)
        {
            return node;
        }

        var m = k;
        for (; m < inorder.Length; m++)
        {
            if (inorder[m] == preorder[i])
            {
                break;
            }
        }

        node.left = BuildTree(preorder, i + 1, i + m - k, inorder, k, m - 1);
        node.right = BuildTree(preorder, i + 1 + m - k, j, inorder, m + 1, l);

        return node;
    }
}