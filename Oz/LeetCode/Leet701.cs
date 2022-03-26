// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet701
{
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    
    public TreeNode InsertIntoBST(TreeNode root, int val) {
        if (root == null)
        {
            root = new TreeNode(val);
            return root;
        }

        var parent = FindTargetNode(root, val);
        var newNode = new TreeNode(val);
        if (val < parent.val)
        {
            parent.left = newNode;
        }
        else
        {
            parent.right = newNode;
        }

        return root;
    }

    private TreeNode FindTargetNode(TreeNode root, int val)
    {
        if (val < root.val)
        {
            return root.left == null ? root : FindTargetNode(root.left, val);
        }
        else
        {
            return root.right == null ? root : FindTargetNode(root.right, val);
        }
    }
}