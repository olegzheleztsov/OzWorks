// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

/*
 * 429. N-ary Tree Level Order Traversal
 * Given an n-ary tree, return the level order traversal of its nodes' values.

Nary-Tree input serialization is represented in their level order traversal, each group of children is separated by the null value (See examples).
 */
public class _429
{
    public static IList<IList<int>> LevelOrder(Node root)
    {
        if (root == null)
        {
            return new List<IList<int>>();
        }
        var queue = new Queue<Node>();
        queue.Enqueue(root);
        var levels = new List<IList<int>>();

        while (queue.Count > 0)
        {
            var level = new List<int>();
            var count = queue.Count;
            for (var i = 0; i < count; i++)
            {
                var node = queue.Dequeue();
                level.Add(node.val);
                if (node.children == null)
                {
                    continue;
                }

                foreach (var child in node.children)
                {
                    if (child != null)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            levels.Add(level);
        }

        return levels;
    }

    public class Node
    {
        public IList<Node> children;
        public int val;

        public Node()
        {
        }

        public Node(int _val) =>
            val = _val;

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }
}