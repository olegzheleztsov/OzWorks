using System;

namespace Oz.Algorithms.DataStructures
{
    public class BinaryTree<T>
    {
        private readonly BinaryTreeNode _root;

        public BinaryTree(BinaryTreeNode root)
        {
            _root = root;
        }

        public void Visit(Action<T> visitor)
        {
            if (_root == null)
            {
                return;
            }

            VisitInner(_root, visitor);
        }

        public void VisitIterative(Action<T> visitor)
        {
            if (_root == null)
            {
                return;
            }

            VisitIterative(_root, visitor);
        }

        private static void VisitInner(BinaryTreeNode node, Action<T> visitor)
        {
            visitor(node.Data);
            if (node.Left != null)
            {
                VisitInner(node.Left, visitor);
            }

            if (node.Right != null)
            {
                VisitInner(node.Right, visitor);
            }
        }

        private static void VisitIterative(BinaryTreeNode node, Action<T> visitor)
        {
            var stack = new OzLinkedListBasedStack<BinaryTreeNode>();
            stack.Push(node);
            while (stack.NonEmpty)
            {
                var element = stack.Pop();
                if (element != null)
                {
                    visitor?.Invoke(element.Data);
                    stack.Push(element.Left);
                    stack.Push(element.Right);
                }
            }
        }

        public class BinaryTreeNode
        {
            public BinaryTreeNode(T data)
            {
                Data = data;
            }

            public T Data { get; }
            public BinaryTreeNode Left { get; set; }
            public BinaryTreeNode Right { get; set; }
        }
    }
}