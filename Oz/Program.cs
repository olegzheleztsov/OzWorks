using Oz.Algorithms.Uf;
using Oz.Uf;
using System;

RunComparingTest();

void RunComparingTest()
{
    int size = 1000;
    int sequenceLength = 30000;
    PairGenerator pairGenerator = new PairGenerator(size);
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

    for (int i = 0; i < 10000; i++)
    {
        int first = Random.Shared.Next(size);
        int second = Random.Shared.Next(size);

        var uf1Connected = uf1.IsConnected(first, second);
        var uf2Connected = uf2.IsConnected(first, second);
        if (uf1Connected != uf2Connected)
        {
            Console.WriteLine($"We have inconsistency for pair: {(first, second)}");
        }
    }
}