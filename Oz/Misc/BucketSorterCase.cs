using Oz.Algorithms;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort;
using System;
using System.Linq;

namespace Oz;

public class BucketSorterCase
{
    public void Run()
    {
        var source = new DefaultRandomSource();
        int[] array = new ShuffledArray<int>(Enumerable.Range(1, 100).Select(e => source.RandomValue(1, 11)).ToArray());
        var sorter = new BucketSorter<int>();
        sorter.Sort(array, k => k, Comparisions.StandardComparision);
        Console.WriteLine(array.GetStringRepresentation());
    }
}