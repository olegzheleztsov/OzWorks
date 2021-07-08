#region

using System;
using System.Diagnostics;
using System.Linq;
using Oz.Algorithms.Rod.DecisionTrees;

#endregion

namespace Oz.Rob
{
    public class DecisionTreeSample
    {
        private readonly Random _random = new Random();

        public void CompareExhaustiveAndBranchAndBound(int arraySize)
        {
            var sourceData = new int[arraySize];
            for (var i = 0; i < arraySize; i++)
            {
                sourceData[i] = _random.Next(0, 101);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = ExhaustiveSearch.StartExhaustiveSearch(sourceData);
            stopwatch.Stop();

            Console.WriteLine(
                $"Exhaustive search takes: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");

            stopwatch.Restart();
            result = ExhaustiveSearch.StartBranchAndBoundSearch(sourceData);
            stopwatch.Stop();
            Console.WriteLine(
                $"Branch and bound: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");
        }
    }
}