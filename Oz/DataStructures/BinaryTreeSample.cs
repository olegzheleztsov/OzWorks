using System;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;

namespace Oz.DataStructures
{
    public class BinaryTreeSample
    {
        private BinaryTree<int> CreateSampleTree()
        {
            var n5 = new BinaryTree<int>.BinaryTreeNode(5);
            var n4 = new BinaryTree<int>.BinaryTreeNode(4);
            var n7 = new BinaryTree<int>.BinaryTreeNode(7);
            var n12 = new BinaryTree<int>.BinaryTreeNode(12);
            var n18 = new BinaryTree<int>.BinaryTreeNode(18);
            var n10 = new BinaryTree<int>.BinaryTreeNode(10);
            var n2 = new BinaryTree<int>.BinaryTreeNode(2);
            var n21 = new BinaryTree<int>.BinaryTreeNode(21);

            n4.Left = n5;
            n12.Left = n7;
            n12.Right = n4;
            n18.Left = n12;
            n18.Right = n10;
            n10.Left = n2;
            n10.Right = n21;
            return new BinaryTree<int>(n18);
        }

        public void Visit()
        {
            var tree = CreateSampleTree();
            tree.Visit(element => Console.Write($"{element}, "));
            Console.WriteLine();
            tree.VisitIterative(element => Console.Write($"{element}, "));
            Console.WriteLine();
        }
    }
}