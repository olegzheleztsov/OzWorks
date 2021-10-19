using Oz.Algorithms;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort.V2;
using Oz.Algorithms.Uf;
using Oz.ConsoleUtils;
using Oz.Sedgewick;
using Oz.Uf;
using System;
using System.Diagnostics;
using System.Linq;

TestNutsAndBolts();

static void TestNutsAndBolts()
{
    var ex = new Ex2315();

    var nuts = Enumerable.Range(1, 10).ToArray();
    var bolts = Enumerable.Range(1, 10).ToArray();
    ex.Match(nuts, bolts);
    Console.WriteLine(nuts.GetStringRepresentation());
    Console.WriteLine(bolts.GetStringRepresentation());
}

static void TestMergeFaster()
{
    var executor = new StopwatchExecutor();
    var ex = new Ex_2_2_10<int>();
    var ex11 = new Ex_2_2_11<int>();
    var merge = new Merge<int>();

    var rSource = new DefaultRandomSource();
    var array = rSource.RandomArray(1000000, 1, 10);

    executor.AggregateExecution("MergeFaster", 20, () =>
    {
        array.Shuffle();
        ex.Sort(array);
    });

    executor.AggregateExecution("ClassicMerge", 20, () =>
    {
        array.Shuffle();
        merge.Sort(array);
    });
    
    executor.AggregateExecution("Improvements", 20, () =>
    {
        array.Shuffle();
        ex11.Sort(array);
    });
}


static void VisualizingSort()
{
    var ex = new Ex_2_1_17();
    var array = Enumerable.Range(0, 30).ToArray().Shuffled();
    ex.VisualizeSorting(array, SortKind.Insertion);

    Console.Clear();
    Console.Write("Enter command: ");
    var command = Console.ReadLine();
    while (command != "quit")
    {
        switch (command)
        {
            case "insertion":
            {
                array = array.Shuffled();
                ex.VisualizeSorting(array, SortKind.Insertion);
            }
                break;
            case "selection":
            {
                array = array.Shuffled();
                ex.VisualizeSorting(array, SortKind.Selection);
            }
                break;
            case "shell":
            {
                array = array.Shuffled();
                ex.VisualizeSorting(array, SortKind.Shell);
            }
                break;
        }

        Console.Clear();
        Console.Write("Enter command: ");
        command = Console.ReadLine();
    }
}


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