using System;
using System.Linq;

namespace Oz.Algorithms.Rod.DecisionTrees
{
    public static class PartitionSearch
    {
        public static PartitionResult HillClimbingSearch(int[] sourceData)
        {
            var solution = new PartitionSearchSolution();
            for (var index = 0; index < sourceData.Length; index++)
            {
                solution.FirstGroup.Add(index);
                var diffFirst = solution.AbsGroupDifference(sourceData);
                solution.FirstGroup.Remove(index);
                solution.SecondGroup.Add(index);
                var diffSecond = solution.AbsGroupDifference(sourceData);
                solution.SecondGroup.Remove(index);

                if (diffFirst < diffSecond)
                {
                    solution.FirstGroup.Add(index);
                }
                else
                {
                    solution.SecondGroup.Add(index);
                }
            }

            return FinalizeBestSolution(sourceData, solution);
        }

        public static PartitionResult PathImprovementsSearch(int[] sourceData)
        {
            var random = new Random();
            var bestSolution = new PartitionSearchSolution(sourceData.Length);
            var testSolution = new PartitionSearchSolution(0);

            var numTrials = 2 * sourceData.Length * sourceData.Length;

            for (var i = 1; i <= numTrials; i++)
            {
                testSolution.Clear();
                for (var index = 0; index < sourceData.Length; index++)
                {
                    if (random.Next() % 2 == 0)
                    {
                        testSolution.FirstGroup.Add(index);
                    }
                    else
                    {
                        testSolution.SecondGroup.Add(index);
                    }
                }

                var hadImprovement = true;
                while (hadImprovement)
                {
                    hadImprovement = false;
                    for (var index = 0; index < sourceData.Length; index++)
                    {
                        var prevDiff = testSolution.AbsGroupDifference(sourceData);
                        if (testSolution.FirstGroup.Contains(index))
                        {
                            testSolution.FirstGroup.Remove(index);
                            testSolution.SecondGroup.Add(index);
                        }
                        else
                        {
                            testSolution.SecondGroup.Remove(index);
                            testSolution.FirstGroup.Add(index);
                        }

                        var currDiff = testSolution.AbsGroupDifference(sourceData);
                        if (currDiff < prevDiff)
                        {
                            hadImprovement = true;
                        }
                        else
                        {
                            if (testSolution.FirstGroup.Contains(index))
                            {
                                testSolution.FirstGroup.Remove(index);
                                testSolution.SecondGroup.Add(index);
                            }
                            else
                            {
                                testSolution.SecondGroup.Remove(index);
                                testSolution.FirstGroup.Add(index);
                            }
                        }
                    }
                }

                if (IsSecondSolutionImprovement(sourceData, bestSolution, testSolution))
                {
                    bestSolution.ReplaceWith(testSolution);
                }
            }

            return FinalizeBestSolution(sourceData, bestSolution);
        }

        public static PartitionResult RandomSearch(int[] sourceData)
        {
            var random = new Random();
            var bestSolution = new PartitionSearchSolution(sourceData.Length);
            var testSolution = new PartitionSearchSolution(0);
            var numTrials = 3 * sourceData.Length * sourceData.Length;

            for (var i = 0; i <= numTrials; i++)
            {
                for (var index = 0; index < sourceData.Length; index++)
                {
                    if (random.Next() % 2 == 0)
                    {
                        testSolution.FirstGroup.Add(index);
                    }
                    else
                    {
                        testSolution.SecondGroup.Add(index);
                    }
                }

                if (IsSecondSolutionImprovement(sourceData, bestSolution, testSolution))
                {
                    bestSolution.ReplaceWith(testSolution);
                }

                testSolution.FirstGroup.Clear();
                testSolution.SecondGroup.Clear();
            }

            return FinalizeBestSolution(sourceData, bestSolution);
        }

        public static PartitionResult StartBranchAndBoundSearch(int[] sourceData)
        {
            var bestSolution = new PartitionSearchSolution(sourceData.Length);
            var testSolution = new PartitionSearchSolution(0);
            BranchAndBoundImplementation(0, sourceData, bestSolution, testSolution);
            return FinalizeBestSolution(sourceData, bestSolution);
        }

        private static void BranchAndBoundImplementation(int nextIndex, int[] sourceData,
            PartitionSearchSolution bestSolution, PartitionSearchSolution testSolution)
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
            var bestSolution = new PartitionSearchSolution(sourceData.Length);
            var testSolution = new PartitionSearchSolution(0);
            ExhaustiveSearchImplementation(0, sourceData, bestSolution, testSolution);
            return FinalizeBestSolution(sourceData, bestSolution);
        }

        private static PartitionResult FinalizeBestSolution(int[] sourceData, PartitionSearchSolution bestSolution) =>
            new(bestSolution.FirstGroup.Select(index => sourceData[index]).ToArray(),
                bestSolution.SecondGroup.Select(index => sourceData[index]).ToArray());

        private static void ExhaustiveSearchImplementation(int nextIndex,
            int[] sourceData,
            PartitionSearchSolution bestSolution,
            PartitionSearchSolution testSolution)
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

        private static bool IsSecondSolutionImprovement(int[] data, PartitionSearchSolution firstSolution,
            PartitionSearchSolution secondSolution) =>
            secondSolution.AbsGroupDifference(data) < firstSolution.AbsGroupDifference(data);
    }
}