using Oz.Algorithms.Arrays;
using Xunit;

namespace Oz.Algorithms.Tests.Arrays
{
    public class MaxSubArrayTests
    {
        [Fact]
        public void Should_Correctly_Find_Max_SubArray()
        {
            int[] arr =
            {
                -1, -1, 1, 2, 1, -1, -1
            };
            var maxSubArray = new MaxSubArray(arr);
            var (maxLeftIndex, maxRightIndex, sum) = maxSubArray.FindMaxCrossingSubarray(1, 3, 5);
            Assert.Equal(2, maxLeftIndex);
            Assert.Equal(4, maxRightIndex);
            Assert.Equal(4, sum);
        }

        [Fact]
        public void Should_Correctly_Find_Max_Sub_Array_When_All_Elements_Are_Negative()
        {
            int[] arr =
            {
                -1, -1, -1, -1, -1, -1, -1
            };
            var maxSubArray = new MaxSubArray(arr);
            var (maxLeftIndex, maxRightIndex, sum) = maxSubArray.FindMaxCrossingSubarray(1, 3, 5);
            Assert.Equal(3, maxLeftIndex);
            Assert.Equal(4, maxRightIndex);
            Assert.Equal(-2, sum);
        }

        [Fact]
        public void Should_Correctly_Find_Max_SubArray_For_Whole_Array()
        {
            int[] arr =
            {
                13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7
            };
            var maxSubArray = new MaxSubArray(arr);
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
            var maxSubArray2 = new MaxSubArray(arr2);
            var (lowIndex, highIndex, maxSum) = maxSubArray2.Value;
            Assert.Equal(1, lowIndex);
            Assert.Equal(1, highIndex);
            Assert.Equal(-3, maxSum);
        }
    }
}