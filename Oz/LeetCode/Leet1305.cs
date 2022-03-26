// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.LeetCode.Trees;
using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1305
{
    public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
    {
        var list = new List<int>();
        Inorder(root1, n => list.Add(n.val));
        Inorder(root2, n => list.Add(n.val));
        list.Sort();
        return list;
    }

    private void Inorder(TreeNode node, Action<TreeNode> action)
    {
        if (node == null)
        {
            return;
        }

        if (node.left != null)
        {
            Inorder(node.left, action);
        }

        action(node);

        if (node.right != null)
        {
            Inorder(node.right, action);
        }
    }
}