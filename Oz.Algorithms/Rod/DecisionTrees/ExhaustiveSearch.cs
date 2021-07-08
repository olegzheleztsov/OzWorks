#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Oz.Algorithms.Rod.DecisionTrees
{
    public static class ExhaustiveSearch
    {
        public static (int[] firstGroupValues, int[] secondGroupValue) StartExhaustiveSearch(int[] sourceData)
        {
            var bestSolution = new ExhaustiveSearchSolution(sourceData.Length);
            var testSolution = new ExhaustiveSearchSolution(0);
            ExhaustiveSearchImplementation(0, sourceData, bestSolution, testSolution);

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

            return (firstGroupValues.ToArray(), secondGroupValues.ToArray());
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

        public void ReplaceWith(ExhaustiveSearchSolution otherSolution)
        {
            FirstGroup.Clear();
            SecondGroup.Clear();
            FirstGroup.AddRange(otherSolution.FirstGroup);
            SecondGroup.AddRange(otherSolution.SecondGroup);
        }
    }
}