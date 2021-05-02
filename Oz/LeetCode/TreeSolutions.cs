#region

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static System.Console;
using static System.String;

#endregion

namespace Oz.LeetCode
{
    public class TreeSolutions
    {
        public static void TestPreorder()
        {
            var root = CreatePreorderTestTree();
            var res1 = PreorderTraversal(root);
            WriteLine(Join(", ", res1));

            var res2 = PreorderTraversalStack(root);
            WriteLine(Join(" ,", res2));
        }

        public void TestInorder()
        {
            var root = CreatePreorderTestTree();
            var res1 = InorderTraversal(root);
            WriteLine(Join(", ", res1));

            var res2 = InorderTraversalStack(root);
            WriteLine(Join(" ,", res2));
        }

        public static void TestPostOrder()
        {
            var root = CreatePreorderTestTree();
            WriteLine(Join(", ", PostorderTraversal(root)));
            WriteLine(Join(", ", PostorderTraversalStack(root)));
        }

        private static TreeNode CreatePreorderTestTree()
        {
            var n1 = new TreeNode(1);
            var n2 = new TreeNode(2);
            var n3 = new TreeNode(3);
            var n4 = new TreeNode(4);
            var n5 = new TreeNode(5);
            var n6 = new TreeNode(6);
            var n7 = new TreeNode(7);
            n1.left = n2;
            n1.right = n3;
            n2.left = n4;
            n2.right = n5;
            n3.left = n6;
            n5.right = n7;
            return n1;
        }

        private static IEnumerable<int> PostorderTraversalStack(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Any())
            {
                var node = stack.Pop();
                if (node.left != null)
                {
                    stack.Push(node.left);
                }

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                result.Add(node.val);
            }

            result.Reverse();
            return result;
        }

        private static IEnumerable<int> PostorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            PostorderTraversalImplementation(root, result);
            return result;

            static void PostorderTraversalImplementation(TreeNode node, ICollection<int> result)
            {
                if (node.left != null)
                {
                    PostorderTraversalImplementation(node.left, result);
                }

                if (node.right != null)
                {
                    PostorderTraversalImplementation(node.right, result);
                }

                result.Add(node.val);
            }
        }

        private IEnumerable<int> InorderTraversalStack(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            var cur = root;
            while (cur != null)
            {
                stack.Push(cur);
                cur = cur.left;
            }

            while (stack.Count > 0)
            {
                var poppedElement = stack.Pop();
                result.Add(poppedElement.val);
                cur = poppedElement.right;
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
            }

            return result;
        }

        private IEnumerable<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            InorderTraversalImplementation(root, result);
            return result;

            static void InorderTraversalImplementation(TreeNode node, ICollection<int> result)
            {
                if (node.left != null)
                {
                    InorderTraversalImplementation(node.left, result);
                }

                result.Add(node.val);
                if (node.right != null)
                {
                    InorderTraversalImplementation(node.right, result);
                }
            }
        }

        private static IEnumerable<int> PreorderTraversalStack(TreeNode root)
        {
            var stack = new Stack<TreeNode>();
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            stack.Push(root);

            while (stack.Count > 0)
            {
                var poppedElement = stack.Pop();
                result.Add(poppedElement.val);
                if (poppedElement.right != null)
                {
                    stack.Push(poppedElement.right);
                }

                if (poppedElement.left != null)
                {
                    stack.Push(poppedElement.left);
                }
            }

            return result;
        }

        private static IEnumerable<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            PreorderTraversalImpl(root, result);
            return result;

            static void PreorderTraversalImpl(TreeNode node, ICollection<int> list)
            {
                if (node != null)
                {
                    list.Add(node.val);
                    if (node.left != null)
                    {
                        PreorderTraversalImpl(node.left, list);
                    }

                    if (node.right != null)
                    {
                        PreorderTraversalImpl(node.right, list);
                    }
                }
            }
        }

        private static IEnumerable<IList<int>> LevelOrder(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            var queueCount = 1;
            var result = new List<IList<int>>();
            while (queue.Any())
            {
                var newQueueCount = 0;
                var levelList = new List<int>();
                for (var i = 0; i < queueCount; i++)
                {
                    var element = queue.Dequeue();
                    levelList.Add(element.val);
                    if (element.left != null)
                    {
                        queue.Enqueue(element.left);
                        newQueueCount++;
                    }

                    if (element.right != null)
                    {
                        queue.Enqueue(element.right);
                        newQueueCount++;
                    }
                }

                queueCount = newQueueCount;
                result.Add(levelList);
            }

            return result;
        }

        private TreeNode CreateLevelOrderTestTree()
        {
            var n3 = new TreeNode(3);
            var n9 = new TreeNode(9);
            var n20 = new TreeNode(20);
            var n15 = new TreeNode(15);
            var n7 = new TreeNode(7);
            n3.left = n9;
            n3.right = n20;
            n20.left = n15;
            n20.right = n7;
            return n3;
        }

        public void TreeLevelOrderTest()
        {
            var tree = CreateLevelOrderTestTree();
            var result = LevelOrder(tree);
            foreach (var level in result)
            {
                WriteLine(Join(", ", level));
            }
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
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
        
        public int MaxDepth(TreeNode root) {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            var leftDepth = 0;
            if (root.left != null)
            {
                leftDepth = MaxDepth(root.left);
            }

            var rightDepth = 0;
            if (root.right != null)
            {
                rightDepth = MaxDepth(root.right);
            }

            return Math.Max(leftDepth, rightDepth) + 1;
        }

        public void TestSymmetric()
        {
            var tr = CreateSymmetric();
            WriteLine(IsSymmetric(tr));

            tr = CreateNonSymmetric();
            WriteLine(IsSymmetric(tr));
        }

        private TreeNode CreateNonSymmetric()
        {
            var n1 = new TreeNode(1);
            var n21 = new TreeNode(2);
            var n22 = new TreeNode(2);
            var n31 = new TreeNode(3);
            var n32 = new TreeNode(3);
            n1.left = n21;
            n1.right = n22;
            n21.right = n31;
            n22.right = n32;
            return n1;
        }

        private TreeNode CreateSymmetric()
        {
            var n1 = new TreeNode(1);
            var n21 = new TreeNode(2);
            var n31 = new TreeNode(3);
            var n41 = new TreeNode(4);
            var n22 = new TreeNode(2);
            var n32 = new TreeNode(3);
            var n42 = new TreeNode(4);

            n1.left = n21;
            n1.right = n22;
            n21.left = n31;
            n21.right = n41;
            n22.left = n42;
            n22.right = n32;
            return n1;
        }
        
        public bool IsSymmetric(TreeNode root)
        {
            return root == null || IsSame(root.left, root.right);
            
            bool IsSame(TreeNode n1, TreeNode n2)
            {
                if (n1 == null && n2 != null || n1 != null && n2 == null || n1 != null && n2 != null && n1.val != n2.val)
                    return false;
        
                return n1 == null && n2 == null || IsSame(n1.left, n2.right) && IsSame(n1.right, n2.left);
            }
        }
        
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
            {
                return false;
            }
            
            return SumCheck(root, 0, targetSum);
            bool SumCheck(TreeNode node, int prevSum, int targetSum)
            {
                if (node == null)
                {
                    return false;
                }
                if (IsLeaf(node))
                {
                    return prevSum + node.val == targetSum;
                }

                return SumCheck(node.left, prevSum + node.val, targetSum)
                       || SumCheck(node.right, prevSum + node.val, targetSum);
            }

            bool IsLeaf(TreeNode root)
                => root.left == null && root.right == null;
        }
    }
}