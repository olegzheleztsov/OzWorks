// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet101
{
    public bool IsSymmetricRec(TreeNode root)
    {
        if (root == null)
        {
            return true;
        }

        return IsSymImpl(root.left, root.right);

        bool IsSymImpl(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            if (left.val == right.val)
            {
                return IsSymImpl(left.left, right.right) && IsSymImpl(left.right, right.left);
            }

            return false;
        }
    }


    public bool IsSymmetric(TreeNode root)
    {
        if (root == null)
        {
            return true;
        }

        if (root.left == null && root.right == null)
        {
            return true;
        }

        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        List<TreeNode> levelNodes = new();
        while (queue.Count > 0)

        {
            levelNodes.Clear();
            while (queue.Count > 0)
            {
                levelNodes.Add(queue.Dequeue());
            }

            if (levelNodes.Count > 1)
            {
                var i = 0;
                var j = levelNodes.Count - 1;
                while (i < j)
                {
                    if ((levelNodes[i] == null && levelNodes[j] != null) ||
                        (levelNodes[i] != null && levelNodes[j] == null) ||
                        (levelNodes[i] != null && levelNodes[j] != null && levelNodes[i].val != levelNodes[j].val))
                    {
                        return false;
                    }

                    i++;
                    j--;
                }
            }

            foreach (var node in levelNodes)
            {
                if (node != null)
                {
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }
        }

        return true;
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