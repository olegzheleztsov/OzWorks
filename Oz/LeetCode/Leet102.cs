// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet102
{
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        if (root == null)
        {
            return new List<IList<int>>();
        }

        var result = new List<IList<int>>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var list = new List<TreeNode>();
            while (queue.Count > 0)
            {
                list.Add(queue.Dequeue());
            }

            foreach (var n in list)
            {
                if (n.left != null)
                {
                    queue.Enqueue(n.left);
                }

                if (n.right != null)
                {
                    queue.Enqueue(n.right);
                }
            }

            result.Add(list.Select(n => n.val).ToList());
        }

        return result;
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