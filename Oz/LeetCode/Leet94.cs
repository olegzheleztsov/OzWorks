// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet94
{
    public IList<int> InorderTraversal(TreeNode root) {
        if (root == null)
        {
            return new List<int>();
        }

        List<int> result = new List<int>();
        Inorder(result, root);
        return result;
    }

    private void Inorder(List<int> accumulator, TreeNode root)
    {
        if (root.left != null)
        {
            Inorder(accumulator, root.left);
        }
        accumulator.Add(root.val);
        if (root.right != null)
        {
            Inorder(accumulator, root.right);
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