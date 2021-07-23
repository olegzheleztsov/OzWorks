// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.Rod.DecisionTrees
{
    public class PartitionSearchSolution
    {
        public PartitionSearchSolution() : this(0){}
        public PartitionSearchSolution(int numberOfElements)
        {
            FirstGroup = new List<int>();
            SecondGroup = new List<int>();

            for (var i = 0; i < numberOfElements; i++)
            {
                FirstGroup.Add(i);
            }
        }

        public void Clear()
        {
            FirstGroup.Clear();
            SecondGroup.Clear();
        }

        public List<int> FirstGroup { get; }
        public List<int> SecondGroup { get; }

        public int AbsGroupDifference(int[] sourceData)
        {
            if (sourceData.Length > 0 && FirstGroup.Count == 0 && SecondGroup.Count == 0)
            {
                return int.MaxValue;
            }
            
            var sum1 = FirstGroup.Sum(index => sourceData[index]);
            var sum2 = SecondGroup.Sum(index => sourceData[index]);
            return Math.Abs(sum1 - sum2);
        }

        public bool IsCanBeatBestSolution(int[] sourceData, PartitionSearchSolution currentBestSolution)
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

        public void ReplaceWith(PartitionSearchSolution otherSolution)
        {
            FirstGroup.Clear();
            SecondGroup.Clear();
            FirstGroup.AddRange(otherSolution.FirstGroup);
            SecondGroup.AddRange(otherSolution.SecondGroup);
        }
    }
}