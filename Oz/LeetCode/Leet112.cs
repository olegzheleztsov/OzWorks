// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet112
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
    
    public bool HasPathSum(TreeNode root, int targetSum) {
        int accum = 0;
        return HasPathSumInner(root, targetSum, accum);
    }

    private bool HasPathSumInner(TreeNode root, int targetSum, int accum)
    {
        if (root == null)
        {
            return false;
        }
        accum += root.val;
        if (root.left == null && root.right == null)
        {
            if (accum == targetSum)
            {
                return true;
            }
        }

        return HasPathSumInner(root.left, targetSum, accum) || HasPathSumInner(root.right, targetSum, accum);
    }
}