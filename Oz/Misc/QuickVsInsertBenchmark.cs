using BenchmarkDotNet.Attributes;
using Oz.Algorithms;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort;
using System.Linq;

namespace Oz;

public class QuickVsInsertBenchmark
{
    private const int N = 10000;
    private readonly InsertionSorter<Data> _insertionSorter = new();
    private readonly QuickSorter<Data> _quickSorter = new();

    private readonly ISorter<Data> _randomizedQuickSorter =
        new QuickSorter<Data>(PartitionStrategy.RandomizedPartition);

    private readonly IRandomSource _randomSource = new DefaultRandomSource();
    private Data[] insertData;
    private Data[] quickData;
    private Data[] randomizedData;

    [IterationSetup(Target = nameof(QuickSortWithSameValues))]
    public void QuickSetup()
    {
        quickData = new Data[N];
        for (var i = 0; i < N; i++)
        {
            quickData[i] = new Data {Key = 1, Value = _randomSource.RandomDouble};
        }
    }

    [IterationSetup(Target = nameof(QuickSortWithDifferentValues))]
    public void QuickDifferentSetup() =>
        quickData = new ShuffledArray<Data>(Enumerable.Range(1, N).Select(key => new Data
        {
            Key = key, Value = _randomSource.RandomDouble
        }).ToArray());

    [IterationSetup(Target = nameof(InsertSortWithDifferentValues))]
    public void InsertDifferentSetup() =>
        insertData = new ShuffledArray<Data>(Enumerable.Range(1, N).Select(key => new Data
        {
            Key = key, Value = _randomSource.RandomDouble
        }).ToArray());

    [IterationSetup(Target = nameof(InsertSortWithSameValues))]
    public void InsertSetup()
    {
        insertData = new Data[N];
        for (var i = 0; i < N; i++)
        {
            insertData[i] = new Data {Key = 1, Value = _randomSource.RandomDouble};
        }
    }

    [IterationSetup(Target = nameof(RandomizedQuickSortWithSameValues))]
    public void RandomizedQuickSortSetup()
    {
        randomizedData = new Data[N];
        for (var i = 0; i < N; i++)
        {
            randomizedData[i] = new Data {Key = 1, Value = _randomSource.RandomDouble};
        }
    }

    [Benchmark]
    public void RandomizedQuickSortWithSameValues() =>
        _randomizedQuickSorter.Sort(randomizedData, data => data.Key, Comparisions.StandardComparision);

    [Benchmark]
    public void QuickSortWithSameValues() =>
        _quickSorter.Sort(quickData, data => data.Key, Comparisions.StandardComparision);

    [Benchmark]
    public void InsertSortWithSameValues() =>
        _insertionSorter.Sort(insertData, data => data.Key, Comparisions.StandardComparision);

    [Benchmark]
    public void QuickSortWithDifferentValues() =>
        _quickSorter.Sort(quickData, data => data.Key, Comparisions.StandardComparision);

    [Benchmark]
    public void InsertSortWithDifferentValues() =>
        _insertionSorter.Sort(insertData, data => data.Key, Comparisions.StandardComparision);


    [IterationSetup(Target = nameof(InsertForDescendingData))]
    public void SetupInsertForDescendingData() =>
        insertData = Enumerable.Range(1, N).OrderByDescending(i => i).Select(i => new Data
        {
            Key = i, Value = _randomSource.RandomDouble
        }).ToArray();

    [Benchmark]
    public void InsertForDescendingData() =>
        _insertionSorter.Sort(insertData, data => data.Key, Comparisions.StandardComparision);

    [IterationSetup(Target = nameof(QuicksortForDescendingData))]
    public void SetupQuicksortForDesceindingData() =>
        quickData = Enumerable.Range(1, N).OrderByDescending(i => i).Select(i => new Data
        {
            Key = i, Value = _randomSource.RandomDouble
        }).ToArray();

    [Benchmark]
    public void QuicksortForDescendingData() =>
        _quickSorter.Sort(quickData, data => data.Key, Comparisions.StandardComparision);

    [IterationSetup(Target = nameof(RandomizedQuicksortForDescendingData))]
    public void SetupRandomizedQuicksortForDescendingData() =>
        randomizedData = Enumerable.Range(1, N).OrderByDescending(i => i).Select(i => new Data
        {
            Key = i, Value = _randomSource.RandomDouble
        }).ToArray();

    [Benchmark]
    public void RandomizedQuicksortForDescendingData() =>
        _randomizedQuickSorter.Sort(randomizedData, data => data.Key, Comparisions.StandardComparision);

    public class Data
    {
        public int Key { get; set; }
        public double Value { get; set; }
    }
}