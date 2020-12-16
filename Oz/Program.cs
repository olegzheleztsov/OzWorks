using System;
using Oz;
using Oz.Algorithms.DataStructures.Trees;

var tree = new RbTree<int>(key => key);

for (int i = 1; i <= 20; i++)
{
    tree.Insert(tree.CreateNode(i, TreeNodeColor.Red));
}

var searcher = BinaryTreeSearcherFactory.Create<RbTreeNode<int>>(tree, node => node.Data, SearchMethod.Recursive);

for (int i = 1; i <= 20; i++)
{
    Console.WriteLine(tree.ToString());
    tree.Delete(searcher.Search(i));
}
Console.WriteLine(tree.ToString());

/*
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(41, TreeNodeColor.Red));
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(38, TreeNodeColor.Red));
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(31, TreeNodeColor.Red));
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(12, TreeNodeColor.Red));
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(19, TreeNodeColor.Red));
Console.WriteLine(tree);
tree.Insert(tree.CreateNode(8, TreeNodeColor.Red));
Console.WriteLine(tree);
*/