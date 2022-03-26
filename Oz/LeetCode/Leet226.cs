// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet226
{
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
        {
            return root;
        }

        (root.left, root.right) = (root.right, root.left);
        InvertTree(root.left);
        InvertTree(root.right);
        return root;
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