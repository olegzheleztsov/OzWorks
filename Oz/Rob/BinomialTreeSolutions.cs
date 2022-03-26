#region

using Oz.Algorithms;
using Oz.Algorithms.Rod;
using System.Collections.Generic;
using static System.Console;
using static System.String;

#endregion

namespace Oz.Rob;

public class BinomialTreeSolutions
{
    public void TestInsertionInBinomialHeap()
    {
        int[] data = {16, 74, 24, 93, 28, 56, 78, 58, 48, 63, 83, 76, 15};
        var heap = new BinomialHeap<int>(Comparisions.DecreaseComparison);

        foreach (var number in data)
        {
            WriteLine($"Add: {number}");
            WriteLine("heap:");
            heap.Enqueue(number);
            WriteLine(heap);
            WriteLine();
        }

        WriteLine();
        WriteLine();
        while (!heap.IsEmpty)
        {
            var number = heap.Dequeue();
            WriteLine($"Dequeued: {number}");
            WriteLine(heap);
            WriteLine();
        }
    }

    private BinomialNode<int> CreateFirstTree()
    {
        var n24 = new BinomialNode<int>(24);
        var n28 = new BinomialNode<int>(28);
        var n56 = new BinomialNode<int>(56);
        var n58 = new BinomialNode<int>(58);

        n24.FirstChild = n28;
        n28.NextSibling = n56;
        n28.FirstChild = n58;
        return n24;
    }

    private BinomialNode<int> CreateSecondTree()
    {
        var n16 = new BinomialNode<int>(16);
        var n74 = new BinomialNode<int>(74);
        var n93 = new BinomialNode<int>(93);
        var n78 = new BinomialNode<int>(78);

        n16.FirstChild = n74;
        n74.NextSibling = n93;
        n74.FirstChild = n78;
        return n16;
    }

    private void PrintNode(string message, BinomialNode<int> node)
    {
        var values = new List<int>();
        node.Visit((value, _) => { values.Add(value); });
        WriteLine($"{message}: {Join(", ", values)}");
    }

    public void TestMerge()
    {
        var first = CreateFirstTree();
        PrintNode("first", first);
        var second = CreateSecondTree();
        PrintNode("second", second);
        var result = BinomialNodeExtensions.MergeTrees(first, second, (a, b) => a.CompareTo(b));
        PrintNode("result", result);
    }
}