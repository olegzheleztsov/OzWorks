// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet404
{
    public int SumOfLeftLeaves(TreeNode root)
    {
        return Sum(root, true);
    }

    private int Sum(TreeNode root, bool isLeft)
    {
        if (root == null)
        {
            return 0;
        }
        if (root.left == null && root.right == null && isLeft)
        {
            return root.val;
        }

        int s1 = 0;
        int s2 = 0;
        if (root.left != null)
        {
            s1 = Sum(root.left, true);
        }

        if (root.right != null)
        {
            s2 = Sum(root.right, false);
        }

        return s1 + s2;
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