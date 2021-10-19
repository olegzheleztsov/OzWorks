using System;

namespace Oz.LeetCode;

/// <summary>
///     Given the root of a binary tree, return the sum of values of its deepest leaves.
/// </summary>
public class Leet_1302
{
    public int DeepestLeavesSum(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        var sum = 0;

        var maxDepth = GetMaxDepth(root);
        SumTraverse(root, 1, maxDepth, ref sum);
        return sum;
    }

    private void SumTraverse(TreeNode root, int depth, int maxDepth, ref int sum)
    {
        if (depth == maxDepth)
        {
            sum += root.val;
            return;
        }

        if (root.left != null)
        {
            SumTraverse(root.left, depth + 1, maxDepth, ref sum);
        }

        if (root.right != null)
        {
            SumTraverse(root.right, depth + 1, maxDepth, ref sum);
        }
    }

    private int GetMaxDepth(TreeNode root)
    {
        if (root.left == null && root.right == null)
        {
            return 1;
        }

        if (root.left != null && root.right == null)
        {
            return GetMaxDepth(root.left) + 1;
        }

        if (root.left == null && root.right != null)
        {
            return GetMaxDepth(root.right) + 1;
        }

        return Math.Max(GetMaxDepth(root.left), GetMaxDepth(root.right)) + 1;
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