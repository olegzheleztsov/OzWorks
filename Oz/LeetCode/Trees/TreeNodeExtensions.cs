// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz     File: TreeNodeExtensions.cs    Created at 2021/05/06/7:32 PM
// All rights reserved, for personal using only
// 

namespace Oz.LeetCode.Trees;

public static class TreeNodeExtensions
{
    public static TreeNode CreateTreeNode(this int value)
        => new(value);

    public static (TreeNode parent, TreeNode child) WithLeft(this TreeNode parent, int leftValue)
    {
        var leftNode = new TreeNode(leftValue);
        parent.left = leftNode;
        return (parent, leftNode);
    }

    public static (TreeNode parent, TreeNode child) WithRight(this TreeNode parent, int rightValue)
    {
        var rightNode = new TreeNode(rightValue);
        parent.right = rightNode;
        return (parent, rightNode);
    }
}