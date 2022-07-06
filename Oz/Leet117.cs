// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

public sealed class Leet117
{
    public Node Connect(Node root)
    {
        if (root == null)
        {
            return null;
        }

        Queue<Node> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var count = queue.Count;
            List<Node> levelNodes = new();
            for (var i = 0; i < count; i++)
            {
                levelNodes.Add(queue.Dequeue());
            }

            for (var i = 1; i < levelNodes.Count; i++)
            {
                levelNodes[i - 1].next = levelNodes[i];
            }

            for (var i = 0; i < levelNodes.Count; i++)
            {
                if (levelNodes[i].left != null)
                {
                    queue.Enqueue(levelNodes[i].left);
                }

                if (levelNodes[i].right != null)
                {
                    queue.Enqueue(levelNodes[i].right);
                }
            }
        }

        return root;
    }

    public class Node
    {
        public Node left;
        public Node next;
        public Node right;
        public int val;

        public Node()
        {
        }

        public Node(int _val) =>
            val = _val;

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
}