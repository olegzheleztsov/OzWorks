// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet104
{
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
    
    public int MaxDepth(TreeNode root) {
        if (root == null)
        {
            return 0;
        }

        return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
    }
}