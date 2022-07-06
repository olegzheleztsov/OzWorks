// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _450
{
    public TreeNode DeleteNode(TreeNode root, int key) {
        if (root == null)
        {
            return null;
        }

        if (key < root.val)
        {
            root.left = DeleteNode(root.left, key);
        } else if (key > root.val)
        {
            root.right = DeleteNode(root.right, key);
        }
        else
        {
            if (root.left == null && root.right == null)
            {
                return null;
            } else if (root.left == null || root.right == null)
            {
                var temp = root.left != null ? root.left : root.right;
                return temp;
            } else if (root.left != null && root.right != null)
            {
                var temp = root.right;
                while (temp.left != null)
                {
                    temp = temp.left;
                }

                root.val = temp.val;
                root.right = DeleteNode(root.right, root.val);
            }
        }

        return root;
    }
    
}