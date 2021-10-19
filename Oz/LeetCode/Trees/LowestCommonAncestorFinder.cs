using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.String;

namespace Oz.LeetCode.Trees;

public class LowestCommonAncestorFinder
{
    private TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var pathP = FindPath(root, p.val);
        var pathQ = FindPath(root, q.val);

        if (pathP.Count == 1)
        {
            return pathP[0];
        }

        if (pathQ.Count == 1)
        {
            return pathQ[0];
        }

        var index = 0;
        var maxIndex = Math.Max(pathP.Count, pathQ.Count);
        while (index < maxIndex)
        {
            if (index >= pathP.Count)
            {
                return pathP[index - 1];
            }

            if (index >= pathQ.Count)
            {
                return pathQ[index - 1];
            }

            if (pathP[index] != pathQ[index])
            {
                return pathP[index - 1];
            }

            index++;
        }

        return null;
    }


    private List<TreeNode> FindPath(TreeNode root, int value)
    {
        var paths = new Dictionary<int, List<TreeNode>>();
        var queue = new Queue<TreeNode>();
        var visited = new HashSet<int>();
        queue.Enqueue(root);
        AddNodeToPath(paths, null, root);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            visited.Add(node.val);

            if (node.left != null && !visited.Contains(node.left.val))
            {
                queue.Enqueue(node.left);
                AddNodeToPath(paths, node, node.left);
            }

            if (node.right != null && !visited.Contains(node.right.val))
            {
                queue.Enqueue(node.right);
                AddNodeToPath(paths, node, node.right);
            }
        }

        return paths[value];
    }

    private static void AddNodeToPath(Dictionary<int, List<TreeNode>> paths, TreeNode parent, TreeNode node)
    {
        if (paths.ContainsKey(node.val))
        {
            paths[node.val].Add(node);
        }
        else
        {
            if (parent != null)
            {
                var newPath = new List<TreeNode>(paths[parent.val]) {node};
                paths.Add(node.val, newPath);
            }
            else
            {
                var newPath = new List<TreeNode> {node};
                paths.Add(node.val, newPath);
            }
        }
    }

    public void TestLowestCommonAncestor()
    {
        var tree = CreateTestTree();
        var p5 = FindPath(tree, 5).Last();
        var p4 = FindPath(tree, 4).Last();
        var lca = LowestCommonAncestor(tree, p5, p4);
        WriteLine(lca.val);
    }

    public void TestFindPath()
    {
        var tree = CreateTestTree();
        var path4 = FindPath(tree, 4);
        WriteLine(Join(",", path4.Select(n => n.val)));

        var path3 = FindPath(tree, 3);
        WriteLine(Join(",", path3.Select(n => n.val)));
    }

    private TreeNode CreateTestTree()
    {
        var rootValue = 3;
        var (root, _) = rootValue.CreateTreeNode().WithLeft(5).parent.WithRight(1);
        var n5 = root.left;
        var (_, n2) = n5.WithLeft(6).parent.WithRight(2);
        n2.WithLeft(7).parent.WithRight(4);
        root.right.WithLeft(0).parent.WithRight(8);
        return root;
    }
}