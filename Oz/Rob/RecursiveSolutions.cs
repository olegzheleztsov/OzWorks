using Oz.Algorithms.Rod;
using System;
using System.Diagnostics;

namespace Oz.Rob;

using static Console;

public class RecursiveSolutions
{
    public void PrintFactorials(int maxNumber)
    {
        for (var i = 0; i <= maxNumber; i++)
        {
            WriteLine($"{i}! = {i.Factorial()}");
        }
    }

    public void PrintFibonacci(int number)
    {
        var stopwatch = new Stopwatch();
        for (var i = 0; i <= number; i++)
        {
            stopwatch.Restart();
            var fibNumber = i.Fibonacci();
            stopwatch.Stop();
            WriteLine($"Fibonacci({i}) = {fibNumber}, time: {stopwatch.Elapsed.TotalSeconds} secs");
        }
    }

    public void PrintHanoi(int discCount)
    {
        for (var i = 1; i <= discCount; i++)
        {
            WriteLine($"Disc count: {i}");
            var hanoi = new HanoiSolver(i);
            hanoi.Run();
            WriteLine();
            WriteLine();
        }
    }
}