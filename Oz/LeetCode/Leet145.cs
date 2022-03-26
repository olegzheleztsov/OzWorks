// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet145
{
    public IList<int> PostorderTraversal(TreeNode root) {
        if (root == null)
        {
            return new List<int>();
        }

        List<int> result = new List<int>();
        Postorder(result, root);
        return result;
    }

    private void Postorder(List<int> accumulator, TreeNode root)
    {
        if (root.left != null)
        {
            Postorder(accumulator, root.left);
        }
        if (root.right != null)
        {
            Postorder(accumulator, root.right);
        }
        accumulator.Add(root.val);
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