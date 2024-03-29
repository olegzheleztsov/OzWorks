﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using System.Threading.Tasks;

namespace Oz.Rob;

[Config(typeof(Config))]
public class SelfOrganizedListsBenchmark
{
    private readonly CountDoubleLinkedList<int> _countDoubleLinkedList = new();

    private readonly MoveToFrontSingleLinkedList<int> _moveToFrontSingleLinkedList = new();
    private readonly DefaultRandomSource _randomSource = new();
    private readonly SwapSingleLinkedList<int> _swapSingleLinkedList = new();

    public SelfOrganizedListsBenchmark()
    {
        for (var i = 1; i <= 100; i++)
        {
            var number = _randomSource.RandomValue(1, 101);
            _moveToFrontSingleLinkedList.InsertLast(number);
            _swapSingleLinkedList.InsertLast(number);
            _countDoubleLinkedList.InsertFirst(new FrequencyData<int> {Data = number, Frequency = 1});
        }
    }

    public async Task TaskConfigureAwait()
    {
        var t = Task.Factory.StartNew(() =>
        {
            for (var i = 0; i < 100; i++)
            {
            }
        });
        await t.ConfigureAwait(false);
    }

    [Benchmark]
    public ISelfOrganizedListNode MoveToFrontBenchmark()
    {
        ISelfOrganizedListNode node = null;
        for (var i = 0; i < 10000; i++)
        {
            var number = _randomSource.RandomValue(1, 101);
            node = _moveToFrontSingleLinkedList.Access(val => val == number);
        }

        return node;
    }

    [Benchmark]
    public ISelfOrganizedListNode SwapBenchmark()
    {
        ISelfOrganizedListNode node = null;
        for (var i = 0; i < 10000; i++)
        {
            var number = _randomSource.RandomValue(1, 101);
            node = _swapSingleLinkedList.Access(val => val == number);
        }

        return node;
    }

    [Benchmark]
    public ISelfOrganizedListNode CountBenchmark()
    {
        ISelfOrganizedListNode node = null;
        for (var i = 0; i < 10000; i++)
        {
            var number = _randomSource.RandomValue(1, 101);
            node = _countDoubleLinkedList.Access(fData => fData.Data == number);
        }

        return node;
    }

    private class Config : ManualConfig
    {
        public Config() =>
            WithOption(ConfigOptions.DisableOptimizationsValidator, true);
    }
}