// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _199
{
    public IList<int> RightSideView(TreeNode root)
    {
        if (root == null)
        {
            return new List<int>();
        }
        
        List<int> result = new();
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var queueCount = queue.Count;
            List<int> levelValues = new();
            for (int i = 0; i < queueCount; i++)
            {
                var node = queue.Dequeue();
                levelValues.Add(node.val);
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
            result.Add(levelValues[levelValues.Count-1]);
        }

        return result;
    }
}