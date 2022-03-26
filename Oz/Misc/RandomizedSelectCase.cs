using Oz.Algorithms.Numerics;
using System;
using System.Linq;

namespace Oz;

public class RandomizedSelectCase
{
    public void Run()
    {
        int[] arr = new ShuffledArray<int>(Enumerable.Range(1, 10).ToArray());
        var fifth = arr.RandomizedSelect(5, i => i);
        var second = arr.RandomizedSelect(2, i => i);
        Console.WriteLine(fifth);
        Console.WriteLine(second);
    }
}