// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet98
{
    public bool IsValidBST(TreeNode root)
    {
        return IsValidBst(root, int.MinValue, int.MaxValue);
    }

    private bool IsValidBst(TreeNode node, long minValue, long maxValue)
    {
        if (node == null)
        {
            return true;
        }

        bool currentValid = node.val >= minValue && node.val <= maxValue;
        if (!currentValid)
        {
            return false;
        }

        bool isLeftValid = true;
        bool isRightValid = true;

        if (node.left != null)
        {
            isLeftValid = IsValidBst(node.left, minValue, node.val - 1);
        }

        if (node.right != null)
        {
            isRightValid = IsValidBst(node.right, node.val + 1, maxValue);
        }

        return isLeftValid && isRightValid;
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