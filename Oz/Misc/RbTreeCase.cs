using Oz.Algorithms.DataStructures.Trees;
using System;

namespace Oz;

public class RbTreeCase
{
    public void PrintTestTree()
    {
        var tree = CreateTestTree();
        Console.WriteLine(tree.ToString());
    }

    private RbTree<int> CreateTestTree()
    {
        var tree = new RbTree<int>(val => val);
        var n2 = tree.CreateNode(2);
        var n3 = tree.CreateNode(3);
        var n4 = tree.CreateNode(4);
        var n6 = tree.CreateNode(6);
        var n7 = tree.CreateNode(7);
        var n11 = tree.CreateNode(11);
        var n9 = tree.CreateNode(9);
        var n18 = tree.CreateNode(18);
        var n12 = tree.CreateNode(12);
        var n14 = tree.CreateNode(14);
        var n17 = tree.CreateNode(17);
        var n19 = tree.CreateNode(19);
        var n22 = tree.CreateNode(22);
        var n20 = tree.CreateNode(20);

        n3.SetLeft(n2);
        n4.SetLeft(n3);
        n4.SetRight(n6);
        n7.SetLeft(n4);

        n22.SetLeft(n20);
        n19.SetRight(n22);
        n14.SetLeft(n12);
        n14.SetRight(n17);
        n18.SetLeft(n14);
        n18.SetRight(n19);
        n11.SetLeft(n9);
        n11.SetRight(n18);
        n7.SetRight(n11);
        tree.SetRoot(n7);
        return tree;
    }
}