// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz.Algorithms     File: BruteForcedMaxSubArray.cs    Created at 2020/10/18/6:18 PM
// All rights reserved, for personal using only
// 

namespace Oz.Algorithms.Arrays
{
    public class BruteForcedMaxSubArray : IMaxSubArray
    {
        private readonly int[] _array;

        public BruteForcedMaxSubArray(int[] array)
        {
            _array = array;
        }

        /// <inheritdoc />
        public (int leftIndex, int rightIndex, int sum) FindMaximumSubArray(int lowIndex, int highIndex)
        {
            var maxSum = int.MinValue;
            var maxLeftIndex = int.MinValue;
            var maxRightIndex = int.MinValue;

            for (var i = lowIndex; i <= highIndex; i++)
            {
                for (var j = i; j <= highIndex; j++)
                {
                    var currentSum = Sum(i, j);
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        maxLeftIndex = i;
                        maxRightIndex = j;
                    }
                }
            }

            return (maxLeftIndex, maxRightIndex, maxSum);
        }

        /// <inheritdoc />
        public (int leftIndex, int rightIndex, int sum) Value => FindMaximumSubArray(0, _array.Length - 1);

        private int Sum(int low, int high)
        {
            var sum = 0;
            for (var i = low; i <= high; i++)
            {
                sum += _array[i];
            }

            return sum;
        }
    }
}