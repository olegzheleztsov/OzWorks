using Oz.LeetCode.Trees;
using System.Collections.Generic;

namespace Oz.LeetCode.Recursion;

public class UniqueBinarySearchTreesSolver
{
    public IList<TreeNode> GenerateTrees(int n)
    {
        if (n == 1)
        {
            return new List<TreeNode> {new(n)};
        }

        var previousSubtrees = GenerateTrees(n - 1);
        var newTrees = new List<TreeNode>();
        foreach (var subTree in previousSubtrees)
        {
            var root = new TreeNode(n) {left = subTree};
            newTrees.Add(root);
        }

        foreach (var subTree in previousSubtrees)
        {
            var node = subTree;
            while (node != null)
            {
                var temp = node.right;
                node.right = new TreeNode(n) {left = temp};
                var copy = CopyTree(subTree);
                newTrees.Add(copy);
                node.right = node.right.left;
                node = node.right;
            }
        }

        return newTrees;
    }

    private TreeNode CopyTree(TreeNode root)
    {
        if (root == null)
        {
            return null;
        }

        var rootCopy = new TreeNode(root.val) {left = CopyTree(root.left), right = CopyTree(root.right)};
        return rootCopy;
    }
}