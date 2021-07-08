#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Oz.Algorithms.Rod.DecisionTrees
{
    public static class ExhaustiveSearch
    {
        public static PartitionResult StartBranchAndBoundSearch(int[] sourceData)
        {
            var bestSolution = new ExhaustiveSearchSolution(sourceData.Length);
            var testSolution = new ExhaustiveSearchSolution(0);
            BranchAndBoundImplementation(0, sourceData, bestSolution, testSolution);
            return FinalizeBestSolution(sourceData, bestSolution);
        }

        private static void BranchAndBoundImplementation(int nextIndex, int[] sourceData, ExhaustiveSearchSolution bestSolution, ExhaustiveSearchSolution testSolution)
        {
            if (nextIndex >= sourceData.Length)
            {
                if (IsSecondSolutionImprovement(sourceData, bestSolution, testSolution))
                {
                    bestSolution.ReplaceWith(testSolution);
                }
            }
            else
            {
                if (!testSolution.IsCanBeatBestSolution(sourceData, bestSolution))
                {
                    return;
                }
                
                testSolution.FirstGroup.Add(nextIndex);
                BranchAndBoundImplementation(nextIndex + 1, sourceData, bestSolution, testSolution);
                testSolution.FirstGroup.Remove(nextIndex);
                
                testSolution.SecondGroup.Add(nextIndex);
                BranchAndBoundImplementation(nextIndex + 1, sourceData, bestSolution, testSolution);
                testSolution.SecondGroup.Remove(nextIndex);
            }
        }
        public static PartitionResult StartExhaustiveSearch(int[] sourceData)
        {
            var bestSolution = new ExhaustiveSearchSolution(sourceData.Length);
            var testSolution = new ExhaustiveSearchSolution(0);
            ExhaustiveSearchImplementation(0, sourceData, bestSolution, testSolution);
            return FinalizeBestSolution(sourceData, bestSolution);
        }

        private static PartitionResult FinalizeBestSolution(int[] sourceData, ExhaustiveSearchSolution bestSolution)
        {
            var firstGroupValues = new List<int>();
            foreach (var index in bestSolution.FirstGroup)
            {
                firstGroupValues.Add(sourceData[index]);
            }

            var secondGroupValues = new List<int>();
            foreach (var index in bestSolution.SecondGroup)
            {
                secondGroupValues.Add(sourceData[index]);
            }

            return new PartitionResult(firstGroupValues.ToArray(), secondGroupValues.ToArray());     
        }

        private static void ExhaustiveSearchImplementation(int nextIndex,
            int[] sourceData,
            ExhaustiveSearchSolution bestSolution,
            ExhaustiveSearchSolution testSolution)
        {
            if (nextIndex >= sourceData.Length)
            {
                if (IsSecondSolutionImprovement(sourceData, bestSolution, testSolution))
                {
                    bestSolution.ReplaceWith(testSolution);
                }
            }
            else
            {
                testSolution.FirstGroup.Add(nextIndex);
                ExhaustiveSearchImplementation(nextIndex + 1, sourceData, bestSolution, testSolution);
                testSolution.FirstGroup.Remove(nextIndex);

                testSolution.SecondGroup.Add(nextIndex);
                ExhaustiveSearchImplementation(nextIndex + 1, sourceData, bestSolution, testSolution);
                testSolution.SecondGroup.Remove(nextIndex);
            }
        }

        private static bool IsSecondSolutionImprovement(int[] data, ExhaustiveSearchSolution firstSolution,
            ExhaustiveSearchSolution secondSolution)
        {
            return secondSolution.AbsGroupDifference(data) < firstSolution.AbsGroupDifference(data);
        }
    }

    public record PartitionResult(int[] FirstGroup, int[] SecondGroup);

    public class ExhaustiveSearchSolution
    {
        public ExhaustiveSearchSolution(int numberOfElements)
        {
            FirstGroup = new List<int>();
            SecondGroup = new List<int>();

            for (var i = 0; i < numberOfElements; i++)
            {
                FirstGroup.Add(i);
            }
        }

        public List<int> FirstGroup { get; }
        public List<int> SecondGroup { get; }

        public int AbsGroupDifference(int[] sourceData)
        {
            var sum1 = FirstGroup.Sum(index => sourceData[index]);
            var sum2 = SecondGroup.Sum(index => sourceData[index]);
            return Math.Abs(sum1 - sum2);
        }

        public bool IsCanBeatBestSolution(int[] sourceData, ExhaustiveSearchSolution currentBestSolution)
        {
            var sum1 = FirstGroup.Sum(index => sourceData[index]);
            var sum2 = SecondGroup.Sum(index => sourceData[index]);
            var unassignedSum = ComputeUnassignedSum(sourceData);
            if (sum1 < sum2)
            {
                sum1 += unassignedSum;
            }
            else
            {
                sum2 += unassignedSum;
            }

            return Math.Abs(sum1 - sum2) <= currentBestSolution.AbsGroupDifference(sourceData);
        }

        private int ComputeUnassignedSum(int[] sourceData)
        {
            var unassignedIndexes = new List<int>();
            for (var i = 0; i < sourceData.Length; i++)
            {
                if (FirstGroup.Contains(i) || SecondGroup.Contains(i))
                {
                    continue;
                }

                unassignedIndexes.Add(i);
            }

            return unassignedIndexes.Sum(index => sourceData[index]);
        }

        public void ReplaceWith(ExhaustiveSearchSolution otherSolution)
        {
            FirstGroup.Clear();
            SecondGroup.Clear();
            FirstGroup.AddRange(otherSolution.FirstGroup);
            SecondGroup.AddRange(otherSolution.SecondGroup);
        }
    }
}