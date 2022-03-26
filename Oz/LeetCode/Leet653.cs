// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet653
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
    
    public bool FindTarget(TreeNode root, int k)
    {
        return Inorder(root, new HashSet<int>(), k);
    }

    private bool Inorder(TreeNode node, HashSet<int> hash, int target)
    {
        if (node == null)
        {
            return false;
        }
        if (hash.Contains(target - node.val))
        {
            return true;
        }
        else
        {
            hash.Add(node.val);
            return Inorder(node.left, hash, target) || Inorder(node.right, hash, target);
        }
    }
}