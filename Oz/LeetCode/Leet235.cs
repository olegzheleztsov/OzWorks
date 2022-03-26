// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet235
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null)
        {
            return null;
        }

        if ((p.val <= root.val && root.val <= q.val) ||
            (q.val <= root.val && root.val <= p.val))
        {
            return root;
        }

        if (root.val < p.val)
        {
            return LowestCommonAncestor(root.right, p, q);
        }

        return LowestCommonAncestor(root.left, p, q);
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