using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Oz.LeetCode.Trees;

namespace Oz.LeetCode.Recursion
{
    public class UniqueBinarySearchTreesSolver
    {
        public IList<TreeNode> GenerateTrees(int n) {
            if (n == 1)
            {
                return new List<TreeNode>()
                {
                    new TreeNode(n)
                };
            }
            else
            {
                var previousSubtrees = GenerateTrees(n - 1);
                List<TreeNode> newTrees = new List<TreeNode>();
                foreach (var subTree in previousSubtrees)
                {
                    var root = new TreeNode(n);
                    root.left = subTree;
                    newTrees.Add(root);
                }
                
                foreach(var subTree in previousSubtrees)
                {
                    var node = subTree;
                    while (node!= null)
                    {
                        var temp = node.right;
                        node.right = new TreeNode(n);
                        node.right.left = temp;
                        var copy = CopyTree(subTree);
                        newTrees.Add(copy);
                        node.right = node.right.left;
                        node = node.right;
                    }
                }

                return newTrees;
            }
        }

        private TreeNode CopyTree(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }
            var rootCopy = new TreeNode(root.val);
            rootCopy.left = CopyTree(root.left);
            rootCopy.right = CopyTree(root.right);
            return rootCopy;
        }
    }
}