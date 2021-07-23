#region

using Oz.Algorithms.Rod.DecisionTrees;
using System;
using System.Diagnostics;
using System.Linq;

#endregion

namespace Oz.Rob
{
    public class DecisionTreeSample
    {
        private readonly Random _random = new();

        public void CompareExhaustiveAndBranchAndBound(int arraySize)
        {
            var sourceData = new int[arraySize];
            for (var i = 0; i < arraySize; i++)
            {
                sourceData[i] = _random.Next(0, 101);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = PartitionSearch.StartExhaustiveSearch(sourceData);
            stopwatch.Stop();

            Console.WriteLine(
                $"Exhaustive search takes: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");

            stopwatch.Restart();
            result = PartitionSearch.StartBranchAndBoundSearch(sourceData);
            stopwatch.Stop();
            Console.WriteLine(
                $"Branch and bound: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");

            stopwatch.Restart();
            result = PartitionSearch.RandomSearch(sourceData);
            stopwatch.Stop();
            Console.WriteLine(
                $"Random search: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");
            
            stopwatch.Restart();
            result = PartitionSearch.PathImprovementsSearch(sourceData);
            stopwatch.Stop();
            Console.WriteLine(
                $"Path improvements: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");
            
            stopwatch.Restart();
            result = PartitionSearch.HillClimbingSearch(sourceData);
            stopwatch.Stop();
            Console.WriteLine(
                $"Hill climbing: {stopwatch.Elapsed.TotalSeconds}, sum1: {result.FirstGroup.Sum()}, sum2: {result.SecondGroup.Sum()}");
        }
    }
}