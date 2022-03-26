// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet144
{
    public IList<int> PreorderTraversal(TreeNode root)
    {
        if (root == null)
        {
            return new List<int>();
        }

        var result = new List<int>();
        Preorder(result, root);
        return result;
    }

    private void Preorder(List<int> accumulator, TreeNode root)
    {
        accumulator.Add(root.val);
        if (root.left != null)
        {
            Preorder(accumulator, root.left);
        }

        if (root.right != null)
        {
            Preorder(accumulator, root.right);
        }
    }

    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int val;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}