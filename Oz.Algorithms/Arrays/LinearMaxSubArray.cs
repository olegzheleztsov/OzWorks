// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz.Algorithms     File: LinearMaxSubArray.cs    Created at 2020/10/18/7:45 PM
// All rights reserved, for personal using only
// 

using System;

namespace Oz.Algorithms.Arrays
{
    public class LinearMaxSubArray : IMaxSubArray
    {
        private readonly int[] _array;

        public LinearMaxSubArray(int[] array)
        {
            _array = array;
        }

        /// <inheritdoc />
        public (int leftIndex, int rightIndex, int sum) FindMaximumSubArray(int lowIndex, int highIndex)
        {
            var totalSum = int.MinValue;
            int? lowSumIndex = null, highSumIndex = null;
            var currentSum = 0;
            var currentSumIndex = lowIndex;
            for (var i = lowIndex; i <= highIndex; i++)
            {
                currentSum += _array[i];
                if (currentSum > totalSum)
                {
                    lowSumIndex = currentSumIndex;
                    highSumIndex = i;
                    totalSum = currentSum;
                }

                if (currentSum < 0)
                {
                    currentSum = 0;
                    currentSumIndex = i + 1;
                }
            }

            if (lowSumIndex == null)
            {
                throw new InvalidOperationException(nameof(lowSumIndex));
            }

            if (highSumIndex == null)
            {
                throw new InvalidOperationException(nameof(highSumIndex));
            }

            return (lowSumIndex.Value, highSumIndex.Value, totalSum);
        }

        /// <inheritdoc />
        public (int leftIndex, int rightIndex, int sum) Value => FindMaximumSubArray(0, _array.Length - 1);
    }
}