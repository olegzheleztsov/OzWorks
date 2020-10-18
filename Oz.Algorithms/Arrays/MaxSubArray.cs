using System;

namespace Oz.Algorithms.Arrays
{
    public class MaxSubArray : IMaxSubArray
    {
        private readonly int[] _array;

        public MaxSubArray(int[] array)
        {
            _array = array;
        }

        public (int maxLeft, int maxRight, int sum) FindMaxCrossingSubarray(int lowIndex, int midIndex, int highIndex)
        {
            var leftSum = int.MinValue;
            var rightSum = int.MinValue;
            var sum = 0;
            var maxLeftIndex = -1;
            var maxRightIndex = -1;
            for (var i = midIndex; i >= lowIndex; i--)
            {
                sum += _array[i];
                if (sum > leftSum)
                {
                    leftSum = sum;
                    maxLeftIndex = i;
                }
            }


            sum = 0;
            for (var j = midIndex + 1; j <= highIndex; j++)
            {
                sum += _array[j];
                if (sum > rightSum)
                {
                    rightSum = sum;
                    maxRightIndex = j;
                }
            }

            return (maxLeftIndex, maxRightIndex, leftSum + rightSum);
        }

        public (int leftIndex, int rightIndex, int sum) FindMaximumSubArray(int lowIndex, int highIndex)
        {
            if (highIndex < lowIndex)
            {
                throw new ArgumentException($"{nameof(highIndex)} < {nameof(lowIndex)}: {highIndex} < {lowIndex}");
            }
            if (lowIndex == highIndex)
            {
                return (lowIndex, highIndex, _array[lowIndex]);
            }

            var mid = (int) Math.Floor((lowIndex + highIndex) / 2.0);
            var (leftLow, leftHigh, leftSum) = FindMaximumSubArray(lowIndex, mid);
            var (rightLow, rightHigh, rightSum) = FindMaximumSubArray(mid + 1, highIndex);
            var (crossLow, crossHigh, crossSum) = FindMaxCrossingSubarray(lowIndex, mid, highIndex);
            if (leftSum >= rightSum && leftSum >= crossSum)
            {
                return (leftLow, leftHigh, leftSum);
            }

            if (rightSum >= leftSum && rightSum >= crossSum)
            {
                return (rightLow, rightHigh, rightSum);
            }

            return (crossLow, crossHigh, crossSum);
        }

        public (int leftIndex, int rightIndex, int sum) Value => FindMaximumSubArray(0, _array.Length - 1);
    }
}