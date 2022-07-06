// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _113
{
    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        List<IList<int>> res = new List<IList<int>>();
        if (root != null)
        {
            Dfs(res, root, targetSum, new List<int>());
        }

        return res;
    }

    private void Dfs(IList<IList<int>> res,  TreeNode n, int sum, List<int> path)
    {
        if (n == null)
        {
            return;
        }
        path.Add(n.val);
        if (n.left == null && n.right == null && sum == n.val)
        {
            res.Add(path);
        }
        Dfs(res, n.left, sum - n.val, new List<int>(path));
        Dfs(res, n.right, sum - n.val, new List<int>(path));
    }
}