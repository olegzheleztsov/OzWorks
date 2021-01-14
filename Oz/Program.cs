using System;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;

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


// char[] arr = {'a', 'b', 'c', 'd', 'e'};
// var segment = new ArraySegment<char>(arr, 1, 0);
// Console.WriteLine(segment.Count);
// Console.WriteLine(string.Join(" ", segment.ToArray()));

// var activities = new List<Activity<int>>()
// {
//     new Activity<int>(1, 4, 1),
//     new Activity<int>(3, 5, 2),
//     new Activity<int>(0, 6, 3),
//     new Activity<int>(5, 7, 4),
//     new Activity<int>(3, 9, 5),
//     new Activity<int>(5, 9, 6),
//     new Activity<int>(6, 10, 7),
//     new Activity<int>(8, 11, 8),
//     new Activity<int>(8, 12, 9),
//     new Activity<int>(2, 14, 10),
//     new Activity<int>(12, 16, 11)
// };
// /*
// var result = activities.SelectActivities();*/
//
// InverseActivitySelector<int> selector = new InverseActivitySelector<int>();
// var result = selector.SelectActivities(activities);
//
// foreach (var a in result.OrderBy(a => a.EndTime))
// {
//     Console.Write(a.Data + ", ");
// }
// Console.WriteLine();

/*
var heap = Heap<int>.MinHeap(new int[] {5, 13, 2, 25, 7, 17, 20, 8, 4}, elem => elem);
Console.WriteLine(string.Join(", ", heap));

for (int i = 0; i < heap.HeapSize; i++)
{
    if (heap.HasLeft(i))
    {
        var (leftValue, leftIndex) = heap.Left(i);
        Console.WriteLine($"heap[{i}]: {heap[i]} <= heap[{leftIndex}]:{leftValue}");
    }

    if (heap.HasRight(i))
    {
        var (rightValue, rightIndex) = heap.Right(i);
        Console.WriteLine($"heap[{i}]: {heap[i]} <= heap[{rightIndex}]:{rightValue}");
    }
}*/

// var fileProcessor = new HuffmanFileProcessor(@"D:\development\warandpeace.txt");
// await fileProcessor.ProcessAsync().ConfigureAwait(false);

/*
BinaryCounter counter = new BinaryCounter(new bool[]
{
    false, false, false, false,
    true, false, false, false
});
for (int i = 0; i < 16; i++)
{
    counter.Decrement();
    Console.WriteLine(counter);
}
*/

/*
int globalCounter = 1;

string GetNextName()
{
    var data = globalCounter.ToString();
    globalCounter++;
    return data;
}

Func<int, BTreeNode<string>> allocate = (int treeDegree) =>
{
    var node = new BTreeNode<string>(true, GetNextName());
    return node;
};

Action<BTreeNode<string>> diskWrite = (node) =>
{
    
};

Func<BTree<string>, BTreeNode<string>, int, BTreeNode<string>> diskRead = (containedTree, parentNode, index) =>
{
    var node = new BTreeNode<string>(true, GetNextName());
    return node;
};

var tree = new BTree<string>( allocate, diskWrite, diskRead, 4);

for (int i = 0; i < 100; i++)
{
    tree.Insert(i * 100);
}

Console.WriteLine(tree);
*/



void TestDoubleLinkedCyclicList()
{
    var list = new OzDoubleCyclicLinkedList<int>(Allocators.DoubleLinkedNodeAllocator);
    for (var i = 0; i < 10; i++)
    {
        list.Insert(i);
    }

    Console.WriteLine($"Count: {list.Count}");
    var node = list.Search(element => element == 0);
    for (var i = 0; i < 20; i++)
    {
        Console.Write($"{node.Data} ");
        node = list.Next(node);
    }

    Console.WriteLine();
    for (var i = 0; i < 20; i++)
    {
        Console.Write($"{node.Data} ");
        node = list.Prev(node);
    }
}

void TestConcatenation()
{
    var first = new OzDoubleCyclicLinkedList<int>(Allocators.DoubleLinkedNodeAllocator);
    first.Insert(1).Insert(2);
    var second = new OzDoubleCyclicLinkedList<int>(Allocators.DoubleLinkedNodeAllocator);
    second.Insert(3, 4, 5);
    var newList = OzDoubleCyclicLinkedList<int>.Concatenate(first, second);
    Console.WriteLine(string.Join(" ", newList));

    OzDoubleCyclicLinkedList<int> f2 = null;
    var s2 = new OzDoubleCyclicLinkedList<int>(Allocators.DoubleLinkedNodeAllocator);
    s2.Insert(10, 20, 30);
    var n2 = OzDoubleCyclicLinkedList<int>.Concatenate(f2, s2);
    Console.WriteLine(string.Join(" ", n2));
}

void TestFibonacciHeapInsertAndExtract()
{
    var fHeap = new FibonacciHeap<int>(key => key);

    var randomSource = new DefaultRandomSource();

    for (int i = 0; i < 20; i++)
    {
        var node = new FibonacciHeapNode<int>(randomSource.RandomValue(1, 100));
        fHeap.Insert(node);
    }

    for (int i = 0; i < 20; i++)
    {
        var minNode = fHeap.ExtractMin();
        Console.Write($"{minNode.Data} ");
        Console.WriteLine("STATE:");
        Console.WriteLine(fHeap);
    }
    Console.WriteLine();
    Console.WriteLine($"Fibonacci heap is empty: {fHeap.IsEmpty}");
}

TestFibonacciHeapInsertAndExtract();