using Oz.Algorithms.Sort.V2;
using Oz.Algorithms.Uf;
using Oz.LeetCode;
using Oz.Sedgewick;
using Oz.Uf;
using System;
using System.Diagnostics;

new Ex_2_1_17().PrintNumber(10);

static double Time<T>(string algo, T[] array) where T : IComparable<T>
{
    var timer = new Stopwatch();
    if (algo.Equals("insertion"))
    {
        Insertion.Sort(array);
    }

    if (algo.Equals("selection"))
    {
        Selection.Sort(array);
    }

    if (algo.Equals("shell"))
    {
        Shell.Sort(array);
    }

    timer.Stop();
    var elapsed = timer.Elapsed;
    return elapsed.TotalSeconds;
}

void RunComparingTest()
{
    const int size = 1000;
    const int sequenceLength = 30000;
    var pairGenerator = new PairGenerator(size);
    var sequence = pairGenerator.GetSequence(sequenceLength);

    var uf1 = new QuickUnionUnionFind();
    var uf2 = new QuickUnionWithPathCompressionUnionFind();

    uf1.Reinitialize(size);
    uf2.Reinitialize(size);

    foreach (var (first, second) in sequence)
    {
        uf1.Union(first, second);
        uf2.Union(first, second);
    }

    for (var i = 0; i < 10000; i++)
    {
        var first = Random.Shared.Next(size);
        var second = Random.Shared.Next(size);

        var uf1Connected = uf1.IsConnected(first, second);
        var uf2Connected = uf2.IsConnected(first, second);
        if (uf1Connected != uf2Connected)
        {
            Console.WriteLine($"We have inconsistency for pair: {(first, second)}");
        }
    }
}