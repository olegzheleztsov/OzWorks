// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Oz.ConsoleUtils;

public class StopwatchExecutor
{
    private readonly List<double> _executionTimes = new();
    private readonly Stopwatch _stopwatch = new();
    private int _executionCount;

    private void Execute(string actionName, Action action)
    {
        _stopwatch.Reset();
        _stopwatch.Start();
        action?.Invoke();
        _stopwatch.Stop();
        _executionCount++;
        _executionTimes.Add(_stopwatch.Elapsed.TotalSeconds);

        Console.WriteLine(
            $"{actionName} takes: {_stopwatch.Elapsed.TotalSeconds:N2} seconds, mean time: {_executionTimes.Sum() / _executionCount:N2}");
    }

    public void AggregateExecution(string actionName, int executionCount, Action action)
    {
        _executionCount = 0;
        _executionTimes.Clear();

        for (var i = 0; i < executionCount; i++)
        {
            Execute(actionName, action);
        }
    }
}