using System;
using System.Collections.Generic;
using Oz.Algorithms.Matrices;
using Oz.Algorithms.Numerics;

/*
double[] prices = {1, 5, 8, 9, 10, 17, 17, 20, 24, 30};
var cutter = new RodCutter(prices);
for (var i = 0; i <= prices.Length; i++)
{
    Console.WriteLine($"Len: {i}: Max Price: {cutter.MemoizedCurRod(i)}, Cutting: {string.Join(", ", cutter.GetOptimalCutting(i))}");
}
*/

/*
var tree = new RbTree<int>(key => key);
for (var i = 1; i <= 100; i++)
{
    tree.Insert(tree.CreateNode(i, TreeNodeColor.Red));
}

var searcher = BinaryTreeSearcherFactory.Create<RbTreeNode<int>>(tree, node => node.Data, SearchMethod.Recursive);
for (var i = 1; i <= 100; i++)
{
    Console.WriteLine(tree.ToString());
    tree.Delete(searcher.Search(i));
}

Console.WriteLine(tree.ToString());
*/

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

/*
List<FloatMatrix> matrices = new List<FloatMatrix>()
{
    new FloatMatrix(30, 35),
    new FloatMatrix(35, 15),
    new FloatMatrix(15, 5),
    new FloatMatrix(5, 10),
    new FloatMatrix(10, 20),
    new FloatMatrix(20, 25)
};

var (costs, sequence) = FloatMatrix.MatrixChainOrder(matrices);
List<string> outputPrinting = new List<string>();
FloatMatrix._ComputeOptimalSequence(sequence, 1, matrices.Count, outputPrinting);
Console.WriteLine(string.Join(" ", outputPrinting))
*/

// byte[] x = {1, 0, 0, 1, 0, 1, 0, 1};
// byte[] y = {0, 1, 0, 1, 1, 0, 1, 1, 0};
//
// var longestSubstring = new LongestCommonSubstring<byte>(x, y);
// var result = longestSubstring.GetLongestSubstring();
// Console.WriteLine(string.Join(" ", result));


char[] arr = {'a', 'b', 'c', 'd', 'e'};
var segment = new ArraySegment<char>(arr, 1, 0);
Console.WriteLine(segment.Count);
Console.WriteLine(string.Join(" ", segment.ToArray()));