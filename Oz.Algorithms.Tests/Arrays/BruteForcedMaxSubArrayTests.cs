// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz.Algorithms.Tests     File: BruteForcedMaxSubArrayTests.cs    Created at 2020/10/18/6:39 PM
// All rights reserved, for personal using only
// 

using Oz.Algorithms.Arrays;
using Xunit;

namespace Oz.Algorithms.Tests.Arrays
{
    public class BruteForcedMaxSubArrayTests
    {
        [Fact]
        public void Should_Correctly_Find_Max_SubArray_For_Whole_Array()
        {
            int[] arr =
            {
                13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7
            };
            var maxSubArray = new BruteForcedMaxSubArray(arr);
            var (lowIndex, highIndex, maxSum) = maxSubArray.Value;
            Assert.Equal(7, lowIndex);
            Assert.Equal(10, highIndex);
            Assert.Equal(43, maxSum);
        }

        [Fact]
        public void Should_Correctly_Find_Max_SubArray_For_Negative_Members_Array()
        {
            int[] arr2 =
            {
                -13, -3, -25, -20, -3, -16, -23, -18, -20, -7, -12, -5, -22, -15, -4, -7
            };
            var maxSubArray2 = new BruteForcedMaxSubArray(arr2);
            var (lowIndex, highIndex, maxSum) = maxSubArray2.Value;
            Assert.Equal(1, lowIndex);
            Assert.Equal(1, highIndex);
            Assert.Equal(-3, maxSum);
        }
    }
}